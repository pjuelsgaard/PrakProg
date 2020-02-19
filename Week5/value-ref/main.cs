class main{
	static int Main(){
		sprint.print();
		var x = new writer(); 
		return 0;
	}
}


public static class sprint{
	static string s="Hello from print class\n";
	public static void print{
		System.Console.Write(s);
	}
}

public class writer{
	public string s="Hello from the writer";
	public void print{
		System.Console.Write(s);
	}
}
