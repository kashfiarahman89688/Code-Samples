package meetup;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.util.Arrays;

public class Event {
	
	
	String zipCode = new String();
	int startTime = 0;
	int endTime = 0;
	
	
	Event(String zipCode,int startTime,int endTime)
	{
		
		this.zipCode = zipCode;
		this.startTime = startTime;
		this.endTime = endTime;
		
	}
	Event()
	{
		
	}
	String[] getEvent(String event_id)throws Exception
	{
	
		File file = new File("event.txt");
		FileReader fileReader = new FileReader(file);
		BufferedReader bufferedReader = new BufferedReader(fileReader);
		String line;
		System.out.println(event_id);
		while ((line = bufferedReader.readLine().trim()) != null)
		{
			System.out.println(line);
			if (line.startsWith(event_id))
				{
				 break;
				}
		}
		
		fileReader.close();
		bufferedReader.close();
		return line.split(" ");
	}
	
	public static void main(String[] args)throws Exception
	{
		Event e1 = new Event();
		String[] eventInfo = e1.getEvent("2");
		System.out.println(Arrays.toString(eventInfo));
		System.out.println(eventInfo[1]);
		//Member m1 = new Member(eventInfo[1]);  
	}
}




