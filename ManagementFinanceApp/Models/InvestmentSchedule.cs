using System;

namespace ManagementFinanceApp.Models
{
  /*Investment schedule - harmonogram lokaty */
  public class InvestmentSchedule
  {
    public int Id { get; set; }
    public string Bank { get; set; }
    public string Name { get; set; }
    public int PeriodDeposit { get; set; }
    public int UnitDeposit { get; set; }
    /*Oprocentowanie Stale W Calym Okresie Lokaty */
    public bool InterestRateInAllPerdiodInvestment { get; set; }
    /*Oprocentowanie W Skali Roku */
    public double InterestRateOnScaleOfYear { get; set; }
    /*Kapitalizacja */
    public int Capitalization { get; set; }
    /*Kwota minimum */
    public double MinAmount { get; set; }
    /*Kwota maksymalna */
    public double MaxAmount { get; set; }
    /* Wymagane Konto Osobiste W Tym Banku */
    public bool RequiredPersonalAccountInCurrentBank { get; set; }
    /* Mozliwosc Wczesniejszego Zerwania Lokaty */
    public bool PossibilityEarlyTerminationInvestment { get; set; }
    /* Warunki Wczesniejszego Zerwania Lokaty */
    public string ConditionEarlyTerminationInvestment { get; set; }
    /*Pozostale Informacje */
    public string RestInformation { get; set; }
    /* Harmonogram Dodany Przez Uzytkownika */
    public bool AddedScheduleFromUser { get; set; }
    public DateTime AddedDate { get; set; }
  }
}