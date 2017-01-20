import java.util.*;

class isInterleave{

	public static void isStrInterleave(String s1,String s2, String s3){
		System.out.println("Params: s1=\"" + s1 + "\",s2=\"" + s2 + "\" s3=\"" + s3+ "\"");
		if(s1.length() == 0 && s2.length() == 0){
			System.out.println(s3);
		}
		if(s1.length()>0)
			{
		  		//System.out.print("from s1");
		  		
				s3= s3+s1.substring(0,1);
				//System.out.println("from s1"+s3);
				printValues(s1.substring(1),s2,s3);
				isStrInterleave(s1.substring(1),s2,s3);
				

			}
		if(s2.length()>0){
				//System.out.print("from s2");
				
				s3 = s3 + s2.substring(0,1);
				//System.out.println("from s2"+s3);
				printValues(s1,s2.substring(1),s3);
				isStrInterleave(s1,s2.substring(1),s3);	
				
			}

		

	}
	public static void main(String[] args){
		String s1 = "abc";
		String s2 = "de";
		String s3 = "";
		int len = s1.length()+s2.length();
		printValues(s1,s2,s3);
		isStrInterleave(s1,s2,s3);
	}

	public static void printValues(String s1,String s2, String s3){
		System.out.println(s1+" "+s2+" "+s3);
	}
}