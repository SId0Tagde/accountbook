namespace Accounts{

    public enum SalaryType{
	Monthly,
	Performance,
	Bonus
        }
    public class Salary{
    public int EmployeeID { get; set; }
	public int Amount { get; set; }
	public SalaryType Type { get; set; }

}
}