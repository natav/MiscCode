// https://www.geeksforgeeks.org/generate-rotations-given-string/
// A simple C# program to generate 
// all rotations of a given string	 
using System; 
using System.Text; 

class GFG 
{ 
// Print all the rotated string. 
public static void printRotatedString(string str) 
{ 
	int len = str.Length; 

	// Generate all rotations one 
	// by one and print 
	StringBuilder sb; 

	for (int i = 0; i < len; i++) 
	{ 
		sb = new StringBuilder(); 

		int j = i; // Current index in str 
		int k = 0; // Current index in temp 

		// Copying the second part from 
		// the point of rotation. 
		for (int k2 = j; k2 < str.Length; k2++) 
		{ 
			sb.Insert(k, str[j]); 
			k++; 
			j++; 
		} 

		// Copying the first part from 
		// the point of rotation. 
		j = 0; 
		while (j < i) 
		{ 
			sb.Insert(k, str[j]); 
			j++; 
			k++; 
		} 

		Console.WriteLine(sb); 
	} 
} 

// Driver Code 
public static void Main(string[] args) 
{ 
	string str = "xXVo3QDHXvDqK3GcIIOT8lAl00rZqInoDeMWGaZsjJw9xwbtV7ZfCpYdQfS5gap"; 
	printRotatedString(str); 
} 
} 

// This code is contributed 
// by Shrikant13 
