package meetup;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

public class WriteFile {
	int id;
	String eventId;
	String name;
	String url;
	String status;
	String address;
	String city;
	String event;
	WriteFile(int id,String eventId,String name,String url,String status,String address,String city)
	{
		this.id = id;
		this.eventId = eventId;
		this.name =name;
		this.url =url;
		this.status= status;
		this.address= address;
		this.city = city;
		this.event = this.id+" "+this.eventId+" "+this.name+" "+this.url+" "+this.status+" "+this.address+" "+this.city;
		
	}
	WriteFile()
	{
		
	}
	void writeEvents() throws IOException
	{
		try{
    		
    		
    		File file =new File("event.txt");
    		
    		//if file doesnt exists, then create it
    		if(!file.exists()){
    			file.createNewFile();
    		}
    		
    		//true = append file
    		FileWriter fileWritter = new FileWriter(file.getName(),true);
    	        BufferedWriter bufferWritter = new BufferedWriter(fileWritter);
    	        bufferWritter.write(event.trim());
    	        bufferWritter.write("\n");
    	        bufferWritter.close();
    	    
	        
    	}catch(IOException e){
    		e.printStackTrace();
    	}
	}
	public void writeMembers(String fileName, Member[] members) throws Exception {
		// TODO Auto-generated method stub
		try{
    		
    		 
    		File file =new File(fileName.trim()+".txt");
    		
    		//if file doesnt exists, then create it
    		if(!file.exists()){
    			file.createNewFile();
    		}
    		
    		//true = append file
    		FileWriter fileWritter = new FileWriter(file.getName(),true);
    	    BufferedWriter bufferWritter = new BufferedWriter(fileWritter);
    	    for (int i = 0 ; i< members.length; i++)
    	    {
    	    	bufferWritter.write(members[i].id+" "+members[i].name.trim()+" "+members[i].CheckinStatus);
    	    	bufferWritter.write("\n");
    	    }
    	    	bufferWritter.write("\n");
    	    	bufferWritter.close();
    	    
	        
    	}catch(IOException e){
    		e.printStackTrace();
    	}

	}
	

}
