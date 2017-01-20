package meetup;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.text.DateFormat;
import java.util.Arrays;
//import java.util.Arrays;
import java.util.Calendar;

import java.util.Scanner;

public class MeetUpLive {
	
	static String api_key = new String("3ed2c29165031a2a245912754a345d");
	static String api_host = new String("https://api.meetup.com");
	
	String getDate()
	{
		String mydate = new String();
		mydate = DateFormat.getDateTimeInstance().format(Calendar.getInstance().getTime());
		return mydate;
	}
	

	
	public static boolean isZipValid(String zip) {
	    boolean retval = false;
	    String zipCodePattern = "\\d{5}(-\\d{4})?";
	    retval = zip.matches(zipCodePattern);
	    return retval;
	  }
	
	String getZipcode()
	{
		System.out.print("Enter Zipcode:");
		@SuppressWarnings("resource")
		Scanner in = new Scanner(System.in);
		String zipCode = in.next();
		
		return zipCode;
	}
	
	String getEventcode() throws Exception
	{
		String ECode = new String();
		System.out.print("Enter Event number:");
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		ECode = input.readLine();
		
		return ECode;
	}
	
	int getCheckinStatus() throws Exception
	{
		String nameNo = new String();
		System.out.print("Enter Member Number:");
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		nameNo = input.readLine();
		return Integer.parseInt(nameNo);
		
	}
	
	int MainMenu() throws Exception
	{
		System.out.println("Select option from following menu");
		System.out.println("1. Show Todays Event");
		System.out.println("2. Enter event number to check-in/check-out");
		System.out.println("3. Return to Main Menu");
		System.out.println("4. Quit");
		System.out.println("Enter your choice");
		String nameNo = new String();
		//System.out.print("Enter Member Number:");
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		nameNo = input.readLine();
		return Integer.parseInt(nameNo);
		
	}
	
	
	
	

	public static void main(String[] args) throws Exception{
		// TODO Auto-generated method stub
		String eventNo = "0"; 
		MeetUpLive ml = new MeetUpLive();
		FormatXML fx = new FormatXML();
		System.out.println("Welcome to meet up live");
		System.out.println(ml.getDate());
		int choice;
		choice= ml.MainMenu();
		while( choice <= 4)
		{
		if (choice == 1)
			{
				String zip = ml.getZipcode();
				while(isZipValid(zip) == false)
				{
					System.out.println("Wrong Zipcode");
					zip = ml.getZipcode();
				}
				System.out.println("Today's Events:");
				fx.XMLparse(zip);
				//eventNo = ml.getEventcode();
				//System.out.print(eventNo);
				//choice = ml.MainMenu();
			}
		else if (choice == 2)
			{
		//		System.out.println("Hello");
				Event e1 = new Event();
			//	System.out.println("Hello1");
				String[] eventInfo = new String[50];
			//	System.out.println("Hello2");
				eventNo = ml.getEventcode();
				eventInfo =	e1.getEvent(eventNo.trim());
				System.out.println(Arrays.toString(eventInfo));
				fx.GetEvent(eventInfo[1].trim());
				System.out.println("----------------------------");
				System.out.println("Members");
				System.out.println("----------------------------");
				fx.XMLparseMember(eventInfo[1]);
		//System.out.println(eventInfo[1]);
		
				int i = ml.getCheckinStatus();
				System.out.println(i+". Name: " + Member.mm[i].name);
				System.out.println("   Previous Status:" + Member.mm[i].getStatus(Member.mm[i].CheckinStatus));
				System.out.println("   Current Status:" + Member.mm[i].ChangeStatus(Member.mm[i].CheckinStatus,i));
				System.out.println("\n");
				
				for (int ii = 0; ii< Member.mm.length ; ii++)
				{
					System.out.println(ii+". Name: " + Member.mm[ii].name);
					System.out.println("   Status:" + Member.mm[ii].getStatus(Member.mm[ii].CheckinStatus));
					System.out.println("\n");
				}
				System.out.println("Want to do another check-in/check-out.y/n");
				BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
				String s = input.readLine().trim();
				while(s.equals("y"))
				{
					
					System.out.println(s);
					i = ml.getCheckinStatus();
					System.out.println(i+". Name: " + Member.mm[i].name);
					System.out.println("   Previous Status:" + Member.mm[i].getStatus(Member.mm[i].CheckinStatus));
					System.out.println("   Current Status:" + Member.mm[i].ChangeStatus(Member.mm[i].CheckinStatus,i));
					System.out.println("\n");
					for (int ii = 0; ii< Member.mm.length ; ii++)
					{
						System.out.println(ii+". Name: " + Member.mm[ii].name);
						System.out.println("   Status:" + Member.mm[ii].getStatus(Member.mm[ii].CheckinStatus));
						System.out.println("\n");
					}
					System.out.println("Want to do another check-in/check-out.y/n");
					s = input.readLine().trim();
				
				}
				WriteFile wr = new WriteFile();
				wr.writeMembers(eventNo, Member.mm);
				//choice = ml.MainMenu();
			}
		else if (choice == 3)
		{
			choice = ml.MainMenu();
		}
		else if (choice == 4)
		{
			System.out.println("Quiting....");
			break;
		}
		choice = ml.MainMenu();
	 }		
	//System.out.println("Quiting....");
  }
}




