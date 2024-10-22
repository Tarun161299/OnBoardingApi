//-----------------------------------------------------------------------
// <copyright file="ZmstCountry.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OnBoardingSystem.Data.Abstractions.Models;

public class ZmstCountry
{
    [ZmstCountryValidation]
    public string Code { get; set; } = null!;

    [ZmstCountryValidation]
    public string Name { get; set; } = null!;

    [ZmstCountryValidation]
    public string SAarccode { get; set; } = null!;

    [ZmstCountryValidation]
    public string SAarcname { get; set; } = null!;

    [ZmstCountryValidation]
    public string Isdcode { get; set; } = null!;

    public int Priority { get; set; }

}
public class ZmstCountryValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var country = (ZmstCountry)validationContext.ObjectInstance;
        bool validator = false;
        if (
            (country.Code.Contains("<script>")) ||
            (country.Code.Contains("</script>")) ||
            (country.Code.Contains("<style>")) ||
            (country.Code.Contains("</style>")) ||
            (country.Name.Contains("<script>")) ||
            (country.Name.Contains("</script>")) ||
            (country.Name.Contains("<style>")) ||
            (country.Name.Contains("</style>")) ||
            (country.SAarccode.Contains("<script>")) ||
            (country.SAarccode.Contains("</script>")) ||
            (country.SAarccode.Contains("<style>")) ||
            (country.SAarccode.Contains("</style>")) ||
            (country.SAarcname.Contains("<script>")) ||
            (country.SAarcname.Contains("</script>")) ||
            (country.SAarcname.Contains("<style>")) ||
            (country.SAarcname.Contains("</style>")) ||
            (country.Isdcode.Contains("<script>")) ||
            (country.Isdcode.Contains("</script>")) ||
            (country.Isdcode.Contains("<style>")) ||
            (country.Isdcode.Contains("</style>"))
            )
        {
            validator = true;
            return new ValidationResult("script not allowed."); ;
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}

