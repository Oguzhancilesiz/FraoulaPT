using FraoulaPT.DTOs.AppRoleDTOs;
using FraoulaPT.DTOs.AppUserDTOs;
using FraoulaPT.DTOs.ChatMediaDTOs;
using FraoulaPT.DTOs.ChatMessageDTOs;
using FraoulaPT.DTOs.ExerciseCategoryDTOs;
using FraoulaPT.DTOs.ExerciseDTOs;
using FraoulaPT.DTOs.MediaDTOs;
using FraoulaPT.DTOs.PackageDTOs;
using FraoulaPT.DTOs.UserPackageDTOs;
using FraoulaPT.DTOs.UserProfileDTOs;
using FraoulaPT.DTOs.UserQuestionDTOs;
using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using FraoulaPT.DTOs.UserWorkoutAssignmentDTOs;
using FraoulaPT.DTOs.WorkoutDayDTOs;
using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using FraoulaPT.DTOs.WorkoutProgramDTOs;
using FraoulaPT.Entity;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.MapsterMap
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // AppRole
            config.NewConfig<AppRole, AppRoleCreateDTO>();
            config.NewConfig<AppRoleCreateDTO, AppRole>();

            config.NewConfig<AppRole, AppRoleUpdateDTO>();
            config.NewConfig<AppRoleUpdateDTO, AppRole>();

            config.NewConfig<AppRole, AppRoleDeleteDTO>();
            config.NewConfig<AppRoleDeleteDTO, AppRole>();

            config.NewConfig<AppRole, AppRoleListDTO>();
            config.NewConfig<AppRoleListDTO, AppRole>();

            config.NewConfig<AppRole, AppRoleDetailDTO>();
            config.NewConfig<AppRoleDetailDTO, AppRole>();

            // AppUser
            config.NewConfig<AppUser, AppUserCreateDTO>();
            config.NewConfig<AppUserCreateDTO, AppUser>();

            config.NewConfig<AppUser, AppUserUpdateDTO>();
            config.NewConfig<AppUserUpdateDTO, AppUser>();

            config.NewConfig<AppUser, AppUserDeleteDTO>();
            config.NewConfig<AppUserDeleteDTO, AppUser>();

            config.NewConfig<AppUser, AppUserListDTO>();
            config.NewConfig<AppUserListDTO, AppUser>();

            config.NewConfig<AppUser, AppUserDetailDTO>();
            config.NewConfig<AppUserDetailDTO, AppUser>();

            // ChatMedia
            config.NewConfig<ChatMedia, ChatMediaCreateDTO>();
            config.NewConfig<ChatMediaCreateDTO, ChatMedia>();

            config.NewConfig<ChatMedia, ChatMediaUpdateDTO>();
            config.NewConfig<ChatMediaUpdateDTO, ChatMedia>();

            config.NewConfig<ChatMedia, ChatMediaDeleteDTO>();
            config.NewConfig<ChatMediaDeleteDTO, ChatMedia>();

            config.NewConfig<ChatMedia, ChatMediaListDTO>();
            config.NewConfig<ChatMediaListDTO, ChatMedia>();

            config.NewConfig<ChatMedia, ChatMediaDetailDTO>();
            config.NewConfig<ChatMediaDetailDTO, ChatMedia>();

            // ChatMessage
            config.NewConfig<ChatMessage, ChatMessageCreateDTO>();
            config.NewConfig<ChatMessageCreateDTO, ChatMessage>();

            config.NewConfig<ChatMessage, ChatMessageUpdateDTO>();
            config.NewConfig<ChatMessageUpdateDTO, ChatMessage>();

            config.NewConfig<ChatMessage, ChatMessageDeleteDTO>();
            config.NewConfig<ChatMessageDeleteDTO, ChatMessage>();

            config.NewConfig<ChatMessage, ChatMessageListDTO>();
            config.NewConfig<ChatMessageListDTO, ChatMessage>();

            config.NewConfig<ChatMessage, ChatMessageDetailDTO>();
            config.NewConfig<ChatMessageDetailDTO, ChatMessage>();

            // Exercise
            config.NewConfig<Exercise, ExerciseCreateDTO>();
            config.NewConfig<ExerciseCreateDTO, Exercise>();

            config.NewConfig<Exercise, ExerciseUpdateDTO>();
            config.NewConfig<ExerciseUpdateDTO, Exercise>();

            config.NewConfig<Exercise, ExerciseDeleteDTO>();
            config.NewConfig<ExerciseDeleteDTO, Exercise>();

            config.NewConfig<Exercise, ExerciseListDTO>();
            config.NewConfig<ExerciseListDTO, Exercise>();

            config.NewConfig<Exercise, ExerciseDetailDTO>();
            config.NewConfig<ExerciseDetailDTO, Exercise>();

            // ExerciseCategory
            config.NewConfig<ExerciseCategory, ExerciseCategoryCreateDTO>();
            config.NewConfig<ExerciseCategoryCreateDTO, ExerciseCategory>();

            config.NewConfig<ExerciseCategory, ExerciseCategoryUpdateDTO>();
            config.NewConfig<ExerciseCategoryUpdateDTO, ExerciseCategory>();

            config.NewConfig<ExerciseCategory, ExerciseCategoryListDTO>();
            config.NewConfig<ExerciseCategoryListDTO, ExerciseCategory>();

            config.NewConfig<ExerciseCategory, ExerciseCategoryDetailDTO>();
            config.NewConfig<ExerciseCategoryDetailDTO, ExerciseCategory>();

            // Media
            config.NewConfig<Media, MediaCreateDTO>();
            config.NewConfig<MediaCreateDTO, Media>();

            config.NewConfig<Media, MediaUpdateDTO>();
            config.NewConfig<MediaUpdateDTO, Media>();

            config.NewConfig<Media, MediaDeleteDTO>();
            config.NewConfig<MediaDeleteDTO, Media>();

            config.NewConfig<Media, MediaListDTO>();
            config.NewConfig<MediaListDTO, Media>();

            config.NewConfig<Media, MediaDetailDTO>();
            config.NewConfig<MediaDetailDTO, Media>();

            // Package
            config.NewConfig<Package, PackageCreateDTO>();
            config.NewConfig<PackageCreateDTO, Package>();

            config.NewConfig<Package, PackageUpdateDTO>();
            config.NewConfig<PackageUpdateDTO, Package>();

            config.NewConfig<Package, PackageDeleteDTO>();
            config.NewConfig<PackageDeleteDTO, Package>();

            config.NewConfig<Package, PackageListDTO>();
            config.NewConfig<PackageListDTO, Package>();

            config.NewConfig<Package, PackageDetailDTO>();
            config.NewConfig<PackageDetailDTO, Package>();

            // UserPackage
            config.NewConfig<UserPackage, UserPackageCreateDTO>().Ignore(dest => dest.PackageId);
            config.NewConfig<UserPackageCreateDTO, UserPackage>();

            config.NewConfig<UserPackage, UserPackageUpdateDTO>();
            config.NewConfig<UserPackageUpdateDTO, UserPackage>();

            config.NewConfig<UserPackage, UserPackageDeleteDTO>();
            config.NewConfig<UserPackageDeleteDTO, UserPackage>();

            config.NewConfig<UserPackage, UserPackageListDTO>();
            config.NewConfig<UserPackageListDTO, UserPackage>();

            config.NewConfig<UserPackage, UserPackageDetailDTO>();
            config.NewConfig<UserPackageDetailDTO, UserPackage>();

            // UserProfile
            config.NewConfig<UserProfile, UserProfileCreateDTO>().Ignore(dest => dest.AppUserId);
            config.NewConfig<UserProfileCreateDTO, UserProfile>();

            config.NewConfig<UserProfile, UserProfileUpdateDTO>();
            config.NewConfig<UserProfileUpdateDTO, UserProfile>();

            config.NewConfig<UserProfile, UserProfileDeleteDTO>();
            config.NewConfig<UserProfileDeleteDTO, UserProfile>();

            config.NewConfig<UserProfile, UserProfileListDTO>();
            config.NewConfig<UserProfileListDTO, UserProfile>();

            config.NewConfig<UserProfile, UserProfileDetailDTO>();
            config.NewConfig<UserProfileDetailDTO, UserProfile>();

            // UserQuestion
            config.NewConfig<UserQuestion, UserQuestionCreateDTO>().Ignore(dest => dest.AskedByUserId);
            config.NewConfig<UserQuestionCreateDTO, UserQuestion>();

            config.NewConfig<UserQuestion, UserQuestionUpdateDTO>();
            config.NewConfig<UserQuestionUpdateDTO, UserQuestion>();

            config.NewConfig<UserQuestion, UserQuestionDeleteDTO>();
            config.NewConfig<UserQuestionDeleteDTO, UserQuestion>();

            config.NewConfig<UserQuestion, UserQuestionListDTO>();
            config.NewConfig<UserQuestionListDTO, UserQuestion>();

            config.NewConfig<UserQuestion, UserQuestionDetailDTO>();
            config.NewConfig<UserQuestionDetailDTO, UserQuestion>();

            // UserWeeklyForm
            config.NewConfig<UserWeeklyForm, UserWeeklyFormCreateDTO>();
            config.NewConfig<UserWeeklyFormCreateDTO, UserWeeklyForm>();

            config.NewConfig<UserWeeklyForm, UserWeeklyFormUpdateDTO>();
            config.NewConfig<UserWeeklyFormUpdateDTO, UserWeeklyForm>();

            config.NewConfig<UserWeeklyForm, UserWeeklyFormListDTO>();
            config.NewConfig<UserWeeklyFormListDTO, UserWeeklyForm>();

            config.NewConfig<UserWeeklyForm, UserWeeklyFormDetailDTO>();
            config.NewConfig<UserWeeklyFormDetailDTO, UserWeeklyForm>();

            // UserWorkoutAssignment
            config.NewConfig<UserWorkoutAssignment, UserWorkoutAssignmentCreateDTO>().Ignore(dest => dest.WorkoutProgramId);
            config.NewConfig<UserWorkoutAssignmentCreateDTO, UserWorkoutAssignment>();

            config.NewConfig<UserWorkoutAssignment, UserWorkoutAssignmentUpdateDTO>();
            config.NewConfig<UserWorkoutAssignmentUpdateDTO, UserWorkoutAssignment>();

            config.NewConfig<UserWorkoutAssignment, UserWorkoutAssignmentDeleteDTO>();
            config.NewConfig<UserWorkoutAssignmentDeleteDTO, UserWorkoutAssignment>();

            config.NewConfig<UserWorkoutAssignment, UserWorkoutAssignmentListDTO>();
            config.NewConfig<UserWorkoutAssignmentListDTO, UserWorkoutAssignment>();

            config.NewConfig<UserWorkoutAssignment, UserWorkoutAssignmentDetailDTO>();
            config.NewConfig<UserWorkoutAssignmentDetailDTO, UserWorkoutAssignment>();

            // WorkoutDay
            config.NewConfig<WorkoutDay, WorkoutDayCreateDTO>();
            config.NewConfig<WorkoutDayCreateDTO, WorkoutDay>();

            config.NewConfig<WorkoutDay, WorkoutDayUpdateDTO>();
            config.NewConfig<WorkoutDayUpdateDTO, WorkoutDay>();

            config.NewConfig<WorkoutDay, WorkoutDayDeleteDTO>();
            config.NewConfig<WorkoutDayDeleteDTO, WorkoutDay>();

            config.NewConfig<WorkoutDay, WorkoutDayListDTO>();
            config.NewConfig<WorkoutDayListDTO, WorkoutDay>();

            config.NewConfig<WorkoutDay, WorkoutDayDetailDTO>();
            config.NewConfig<WorkoutDayDetailDTO, WorkoutDay>();

            // WorkoutExercise
            config.NewConfig<WorkoutExercise, WorkoutExerciseCreateDTO>().Ignore(dest => dest.ExerciseId);
            config.NewConfig<WorkoutExerciseCreateDTO, WorkoutExercise>();

            config.NewConfig<WorkoutExercise, WorkoutExerciseUpdateDTO>();
            config.NewConfig<WorkoutExerciseUpdateDTO, WorkoutExercise>();

            config.NewConfig<WorkoutExercise, WorkoutExerciseDeleteDTO>();
            config.NewConfig<WorkoutExerciseDeleteDTO, WorkoutExercise>();

            config.NewConfig<WorkoutExercise, WorkoutExerciseListDTO>();
            config.NewConfig<WorkoutExerciseListDTO, WorkoutExercise>();

            config.NewConfig<WorkoutExercise, WorkoutExerciseDetailDTO>();
            config.NewConfig<WorkoutExerciseDetailDTO, WorkoutExercise>();


            // WorkoutProgram
            config.NewConfig<WorkoutProgram, WorkoutProgramCreateDTO>().Ignore(dest => dest.UserWeeklyFormId);
            config.NewConfig<WorkoutProgramCreateDTO, WorkoutProgram>();

            config.NewConfig<WorkoutProgram, WorkoutProgramUpdateDTO>();
            config.NewConfig<WorkoutProgramUpdateDTO, WorkoutProgram>();

            config.NewConfig<WorkoutProgram, WorkoutProgramDeleteDTO>();
            config.NewConfig<WorkoutProgramDeleteDTO, WorkoutProgram>();

            config.NewConfig<WorkoutProgram, WorkoutProgramListDTO>();
            config.NewConfig<WorkoutProgramListDTO, WorkoutProgram>();

            config.NewConfig<WorkoutProgram, WorkoutProgramDetailDTO>();
            config.NewConfig<WorkoutProgramDetailDTO, WorkoutProgram>();


            // WorkoutProgram → WorkoutProgramDetailDTO
            config.NewConfig<WorkoutProgram, WorkoutProgramDetailDTO>();
            config.NewConfig<WorkoutProgramDetailDTO, WorkoutProgram>();

            // --- EKLENEN KISIM ---

            // Media → MediaDTO
            config.NewConfig<Media, MediaDTO>();

            // UserWeeklyForm → UserWeeklyFormListDTO (ProgressPhotoUrls)
            config.NewConfig<UserWeeklyForm, UserWeeklyFormListDTO>()
                .Map(dest => dest.ProgressPhotoUrls, src =>
                    src.ProgressPhotos != null
                        ? src.ProgressPhotos.Select(m => m.Url).ToList()
                        : new List<string>());

            // UserWeeklyForm → UserWeeklyFormDetailDTO
            config.NewConfig<UserWeeklyForm, UserWeeklyFormDetailDTO>()
                .Map(dest => dest.ProgressPhotoUrls, src => src.ProgressPhotos);
        }
    }
}
