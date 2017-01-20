package meetup;
import java.net.*;
import java.io.*;
/**
 * 
 */


/**
 * @author Kashfia Rahman
 *
 */
public class MeetUpOutput {

	MeetUpOutput(){
	}
	
void getOutput(String url)throws Exception
{
	URL yahoo = new URL(url);
    URLConnection yc = yahoo.openConnection();
    BufferedReader in = new BufferedReader(
                            new InputStreamReader(
                            yc.getInputStream()));
    String inputLine;
    
    File file = new File("input.txt");
    // creates the file
    file.createNewFile();
    // creates a FileWriter Object
    Writer writer = new BufferedWriter(
            new OutputStreamWriter(new FileOutputStream(
                    "input.txt"), "UTF-8"));

    while ((inputLine = in.readLine()) != null)
    {
      writer.write(inputLine);
    }
    writer.flush();
    writer.close();
    in.close(); 
}

/*public static void main(String args[])throws Exception
{
 MeetUpOutput m1 = new MeetUpOutput();
 m1.getOutput("https://api.meetup.com/2/members?&sign=true&photo-host=public&group_id=176399&page=20&format=xml&key=3ed2c29165031a2a245912754a345d");
}*/

}


