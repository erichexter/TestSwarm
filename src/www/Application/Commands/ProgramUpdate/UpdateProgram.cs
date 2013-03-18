using System.ComponentModel.DataAnnotations;

namespace nTestSwarm.Application.Commands.ProgramUpdate
{
    public class UpdateProgram : ProgramCommand
    {
        [Required]
        public int ProgramId { get; set; }
    }
}