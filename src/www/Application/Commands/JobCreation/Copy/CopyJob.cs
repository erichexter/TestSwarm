using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.ComponentModel.DataAnnotations;

namespace nTestSwarm.Application.Commands.JobCreation.Copy
{
    public class CopyJob : IRequest<CreateJobResult>
    {
        [Required]
        public long JobId { get; set; }
        
        [Required]
        public string JobNameFormat { get; set; }

        public object[] JobNameParams { get; set; }
    }
}