using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using WorkoutAppService.Entities;
using WorkoutAppService.Models.DTOs;

namespace WorkoutAppService.Core
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateUserMaps();
            CreateEquipmentMaps();
            CreateMuscleMaps();
            CreateExerciseCategoryMaps();
            CreateExerciseMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Roles, opt =>
                    opt.MapFrom(u => u.UserRoles.Select(ur => ur.Role.Name)));
            CreateMap<RegisterUserDto, User>();
            CreateMap<Role, RoleDto>();
            CreateMap<LinkedAccount, LinkedAccountDto>();
            CreateMap<JsonPatchDocument<UpdateUserDto>, JsonPatchDocument<User>>();
            CreateMap<Operation<UpdateUserDto>, Operation<User>>();
        }

        private void CreateMuscleMaps()
        {
            CreateMap<Muscle, MuscleForReturnDto>();
            CreateMap<MuscleForCreateDto, Muscle>();
            CreateMap<JsonPatchDocument<MuscleForUpdateDto>, JsonPatchDocument<Muscle>>();
            CreateMap<Operation<MuscleForUpdateDto>, Operation<Muscle>>();
        }

        private void CreateEquipmentMaps()
        {
            CreateMap<Equipment, EquipmentForReturnDto>();
            CreateMap<EquipmentForCreationDto, Equipment>();
            CreateMap<JsonPatchDocument<EquipmentForUpdateDto>, JsonPatchDocument<Equipment>>();
            CreateMap<Operation<EquipmentForUpdateDto>, Operation<Equipment>>();
        }

        private void CreateExerciseCategoryMaps()
        {
            CreateMap<ExerciseCategory, ExerciseCategoryForReturnDto>();
            CreateMap<ExerciseCategoryForCreationDto, ExerciseCategory>();
            CreateMap<JsonPatchDocument<ExerciseCategoryForUpdateDto>, JsonPatchDocument<ExerciseCategory>>();
            CreateMap<Operation<ExerciseCategoryForUpdateDto>, Operation<ExerciseCategory>>();
        }

        private void CreateExerciseMaps()
        {
            CreateMap<Exercise, ExerciseForReturnDto>();
            CreateMap<ExerciseForCreationDto, Exercise>();

            CreateMap<ExerciseStep, ExerciseStepForReturnDto>();
        }
    }
}