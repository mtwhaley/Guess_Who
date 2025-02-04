namespace GameExceptions {
    class InvalidInputException: Exception {
        public InvalidInputException(string message="I'm not quite sure what that means. Can you try again?") : base(message) {}

    }
}