public static double Calculate(string userInput)
{
	var numbers = userInput.Split();
	var sum = double.Parse(numbers[0]);
	var months = double.Parse(numbers[2]);
	var percent = double.Parse(numbers[1]);
	
	return sum * Math.Pow((1 + percent / (12 * 100)), months);
}