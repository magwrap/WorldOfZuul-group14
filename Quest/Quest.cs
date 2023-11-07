namespace WoZmyDev1
{
    class Quest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        public void Start()
        {
            // Logic to initiate the quest
            Console.WriteLine($"Quest started: {Title}");
        }

        public void Complete()
        {
            // Logic to complete the quest
            IsCompleted = true;
            Console.WriteLine($"Quest completed: {Title}");
        }
    }
}