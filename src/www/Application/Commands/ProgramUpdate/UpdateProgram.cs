namespace nTestSwarm.Application.Commands.ProgramUpdate
{
    public class UpdateProgram
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public string JobDescriptionUrl { get; set; }
        public int DefaultMaxRuns { get; set; }
    }
}