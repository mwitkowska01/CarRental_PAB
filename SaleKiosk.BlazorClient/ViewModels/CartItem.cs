using CarRental.SharedKernel.Dto;
using System.ComponentModel.DataAnnotations;

namespace CarRental.BlazorClient.ViewModels
{
    public class CartItem
    {
        public ServiceDto Service { get; set; }
        public int Count { get; set; }
    }
}
