using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Kruger.Marketplace.Application.ViewModels
{
    public abstract class EntityViewModel
    {
        [Key]
        [ValidateNever]
        public Guid Id { get; set; }

        protected EntityViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
