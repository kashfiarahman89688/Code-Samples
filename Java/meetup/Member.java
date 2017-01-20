package meetup;

import java.util.Arrays;

public class Member {
	
	String[] NameMember;
	String name = new String();
	int id;
	Boolean CheckinStatus;
	public static Member[] mm ;
	Member(String[] name)
	{
		NameMember = name;
	}
	
	Member()
	{
		this.name="";
		this.id = 0;
		this.CheckinStatus = false;
	 	
	}
	String getStatus(boolean result){
		if (result == true)
			return "Checked in";
		else
			return "Not checked in";
	}
	
	String ChangeStatus(boolean result,int i){
		if (result == true)
		{
			mm[i].CheckinStatus = false;
			return "Not checked in";
		}
		else
		{
			mm[i].CheckinStatus = true;
			return "Checked in";
		}
	}
	
	Member[] SetMember()
	{
		System.out.println(Arrays.toString(NameMember));
		//Member[] 
		mm = new Member[NameMember.length];
		for(int i = 0 ; i< NameMember.length ; i++)
		{
		//	System.out.println(NameMember.length);
		//	System.out.println(NameMember[i]);
		//	System.out.println(i);
			mm[i] = new Member();
			mm[i].name = NameMember[i];
			mm[i].id = i;
			mm[i].CheckinStatus = false;
			System.out.println(i+". Name: " + mm[i].name);
			System.out.println("   Status:" + mm[i].getStatus(mm[i].CheckinStatus));
			System.out.println("\n");
		}
		//System.out.println(Arrays.toString(mm));
		return mm;
		
	}
	

	

}
