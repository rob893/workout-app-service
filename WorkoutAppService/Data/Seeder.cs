using System.Collections.Generic;
using WorkoutAppService.Entities;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WorkoutAppService.Extensions;

namespace WorkoutAppService.Data
{
    public class Seeder
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;


        public Seeder(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedDatabase(bool seedData, bool clearCurrentData, bool applyMigrations, bool dropDatabase)
        {
            if (dropDatabase)
            {
                context.Database.EnsureDeleted();
            }

            if (applyMigrations)
            {
                context.Database.Migrate();
            }

            if (clearCurrentData)
            {
                ClearAllData();
            }

            if (seedData)
            {
                SeedRoles();
                SeedUsers();
                SeedEquipment();
                SeedMuscles();
                SeedExerciseCategories();
                SeedExercises();
            }
        }

        private void ClearAllData()
        {
            context.RefreshTokens.Clear();
            context.Users.Clear();
            context.Roles.Clear();
            context.Exercises.Clear();
            context.Equipment.Clear();
            context.Muscles.Clear();
            context.ExerciseCategories.Clear();

            context.SaveChanges();
        }

        private void SeedRoles()
        {
            if (context.Roles.Any())
            {
                return;
            }

            string data = File.ReadAllText("Data/SeedData/RoleSeedData.json");
            var roles = JsonConvert.DeserializeObject<List<Role>>(data);

            foreach (var role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            }
        }

        private void SeedUsers()
        {
            if (userManager.Users.Any())
            {
                return;
            }

            string data = File.ReadAllText("Data/SeedData/UserSeedData.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(data);

            foreach (User user in users)
            {
                userManager.CreateAsync(user, "password").Wait();

                if (user.UserName.ToUpper() == "ADMIN")
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
                else
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }

        private void SeedEquipment()
        {
            if (context.Equipment.Any())
            {
                return;
            }

            var data = File.ReadAllText("Data/SeedData/EquipmentSeedData.json");
            var equipment = JsonConvert.DeserializeObject<List<Equipment>>(data);

            context.Equipment.AddRange(equipment);

            context.SaveChanges();
        }

        private void SeedMuscles()
        {
            if (context.Muscles.Any())
            {
                return;
            }

            var data = File.ReadAllText("Data/SeedData/MuscleSeedData.json");
            var muscles = JsonConvert.DeserializeObject<List<Muscle>>(data);

            context.Muscles.AddRange(muscles);

            context.SaveChanges();
        }

        private void SeedExerciseCategories()
        {
            if (context.ExerciseCategories.Any())
            {
                return;
            }

            var data = File.ReadAllText("Data/SeedData/ExerciseCategorySeedData.json");
            var exerciseCategories = JsonConvert.DeserializeObject<List<ExerciseCategory>>(data);

            context.ExerciseCategories.AddRange(exerciseCategories);

            context.SaveChanges();
        }

        private void SeedExercises()
        {
            if (context.Exercises.Any())
            {
                return;
            }

            var data = File.ReadAllText("Data/SeedData/ExerciseSeedData.json");
            var exercises = JsonConvert.DeserializeObject<List<Exercise>>(data);
            // This is needed to ensure we don't try to create a new entity with same PK as one being tracked.
            var muscleDict = context.Muscles.ToDictionary(m => m.Id);
            var equipmentDict = context.Equipment.ToDictionary(m => m.Id);
            var categoryDict = context.ExerciseCategories.ToDictionary(m => m.Id);

            foreach (var exercise in exercises)
            {
                if (exercise.PrimaryMuscle != null && muscleDict.ContainsKey(exercise.PrimaryMuscle.Id))
                {
                    exercise.PrimaryMuscle = muscleDict[exercise.PrimaryMuscle.Id];
                }

                if (exercise.SecondaryMuscle != null && muscleDict.ContainsKey(exercise.SecondaryMuscle.Id))
                {
                    exercise.SecondaryMuscle = muscleDict[exercise.SecondaryMuscle.Id];
                }

                var equipmentToAdd = exercise.Equipment
                    .Where(equipment => equipmentDict.ContainsKey(equipment.Id))
                    .Select(equipment => equipmentDict[equipment.Id]).ToList();

                exercise.Equipment.RemoveAll(equipment => equipmentDict.ContainsKey(equipment.Id));

                exercise.Equipment.AddRange(equipmentToAdd);

                var categoriesToAdd = exercise.ExerciseCategorys
                    .Where(cat => categoryDict.ContainsKey(cat.Id))
                    .Select(cat => categoryDict[cat.Id]).ToList();

                exercise.ExerciseCategorys.RemoveAll(cat => categoryDict.ContainsKey(cat.Id));

                exercise.ExerciseCategorys.AddRange(categoriesToAdd);

                context.Exercises.Add(exercise);
            }

            context.SaveChanges();
        }
    }
}