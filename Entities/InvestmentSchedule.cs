using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /*Investment schedule - harmonogram lokaty */
  public class InvestmentSchedule
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Bank { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int PeriodDeposit { get; set; }

    [Required]
    public int UnitDeposit { get; set; }

    /*Oprocentowanie Stale W Calym Okresie Lokaty */
    [Required]
    public bool InterestRateInAllPerdiodInvestment { get; set; }
    /*Oprocentowanie W Skali Roku */
    [Required]
    public double InterestRateOnScaleOfYear { get; set; }
    /*Kapitalizacja */
    [Required]
    public int Capitalization { get; set; }
    /*Kwota minimum */
    [Required]
    public double MinAmount { get; set; }
    /*Kwota maksymalna */
    [Required]
    public double MaxAmount { get; set; }
    /* Wymagane Konto Osobiste W Tym Banku */
    [Required]
    public bool RequiredPersonalAccountInCurrentBank { get; set; }
    /* Mozliwosc Wczesniejszego Zerwania Lokaty */
    [Required]
    public bool PossibilityEarlyTerminationInvestment { get; set; }
    /* Warunki Wczesniejszego Zerwania Lokaty */
    public string ConditionEarlyTerminationInvestment { get; set; }
    /*Pozostale Informacje */
    public string RestInformation { get; set; }
    /* Harmonogram Dodany Przez Uzytkownika */
    [Required]
    public bool AddedScheduleFromUser { get; set; }

    [Required]
    public DateTime AddedDate { get; set; }

    public List<Investment> Investments { get; set; }
  }
}