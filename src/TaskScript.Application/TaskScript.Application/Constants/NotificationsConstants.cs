namespace TaskScript.Application.Constants
{
    public static class NotificationsConstants
    {
        public const string SuccessNotification = "Success";
        public const string ErrorNotification = "Error";
        public const string WarningNotification = "Warning";
        public const string InfoNotification = "Info";

        public const string SuccessfullyAddedRoles = "Successfully added roles!";
        public const string SuccessfullyAddedLesson = "Successfully added lesson!";
        public const string AlreadyCreatedRoles = "Default roles are already created.";
        public const string SuccessfullyEnrolledInLesson = "Successfully enrolled in the lesson.";
        public const string SuccessfullyDisenrolledFromLesson = "Successfully disenrolled from the lesson.";
        public const string AlreadyDisenrolledFromLesson = "Already disenrolled from this lesson!";
        public const string NoSeatsLeft = "There are no seats left.";
        public const string InvalidSeatsValue = "Can not update seats, because there are already {0} enrolled users in the lesson.";
    }
}
