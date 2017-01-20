package meetup;
import java.io.File;

import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.DocumentBuilder;
import org.w3c.dom.Document;
import org.w3c.dom.NodeList;
import org.w3c.dom.Node;
import org.w3c.dom.Element;

public class FormatXML {
	
	public static int count =0;
	MeetUpOutput m1 = new MeetUpOutput();

	FormatXML()
	{
		
	}
	
	void XMLparse(String zip){
		 try {	
			 if(count == 0)
			 {
				 
			 //System.out.println(URLgenerator.EVENT.eventUrlGen("95050", "1469860650000", "1469860660000"));
				 m1.getOutput(URLgenerator.EVENT.eventUrlGen(zip, "0", "1d"));
			     
			 }
			 
				 
			 File inputFile = new File("input.txt");
	         DocumentBuilderFactory dbFactory 
	            = DocumentBuilderFactory.newInstance();
	         DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
	         Document doc = dBuilder.parse(inputFile);
	         doc.getDocumentElement().normalize();
	         NodeList nList = doc.getElementsByTagName("head");
	     	//System.out.println("----------------------------");
	        
	     		for (int temp = 0; temp < nList.getLength(); temp++) {
	     		Node nNode = nList.item(temp);
	     				
//	     		System.out.println("\nCurrent Element :" + nNode.getNodeName());
	     	if (nNode.getNodeType() == Node.ELEMENT_NODE) {
	     			Element eElement = (Element) nNode;
	     			if(count == 0)
	     				{
	     					System.out.println("The total number of events are " + eElement.getElementsByTagName("total_count").item(0).getTextContent());
	     					count++;
	     				}
	     			//System.out.println("The number items in this list are  " + eElement.getElementsByTagName("count").item(0).getTextContent());
	     			//System.out.println("The description of the " + eElement.getElementsByTagName("method").item(0).getTextContent() + " method is : " + eElement.getElementsByTagName("description").item(0).getTextContent());
	     			//System.out.println("Last update : " + eElement.getElementsByTagName("updated").item(0).getTextContent());
	     			//System.out.println("For the next set of events please use : " );
	     			//System.out.println(eElement.getElementsByTagName("next").item(0).getTextContent());
	     			
	     			if(eElement.getElementsByTagName("next").item(0).getTextContent()=="")
	     			{
	     				continue;
	     			}
	     			else
	     				m1.getOutput(eElement.getElementsByTagName("next").item(0).getTextContent());
	     			    this.XMLparse(zip);
	     			}
	     		}
	     	
	     	nList = doc.getElementsByTagName("item");
	    	for (int temp = 0; temp < nList.getLength(); temp++) {

	    		Node nNode = nList.item(temp);
	        // System.out.println("\nCurrent Element :" + nNode.getNodeName());
	        System.out.println("\n");
	        if (nNode.getNodeType() == Node.ELEMENT_NODE) {
	          Element eElement = (Element) nNode;
	          
	          String nameVar;
	          if ( eElement.getElementsByTagName("name").item(0)  == null )
	        	  nameVar = " "; 
	      	  else 
	      	  nameVar = eElement.getElementsByTagName("name").item(0).getTextContent();
	          String eventURLVar;
	          if ( eElement.getElementsByTagName("event_url").item(0)  == null )
	        	  eventURLVar = " "; 
	          else 
	        	  eventURLVar = eElement.getElementsByTagName("event_url").item(0).getTextContent();
	          String statusVar;
	          if ( eElement.getElementsByTagName("status").item(0)  == null )
	        	  statusVar = " "; 
	          else 
	        	  statusVar = eElement.getElementsByTagName("status").item(0).getTextContent();
	          String addressVar;
	          if ( eElement.getElementsByTagName("address_1").item(0)  == null )
	      		addressVar = " "; 
	      	  else 
	      	  addressVar = eElement.getElementsByTagName("address_1").item(0).getTextContent();
	          String cityVar;
	          if ( eElement.getElementsByTagName("city").item(0)  == null )
	        	  cityVar = " "; 
	      	  else 
	      	  cityVar = eElement.getElementsByTagName("city").item(0).getTextContent();
	          
	          String idVar = eElement.getElementsByTagName("id").item(0).getTextContent();
	          
	          System.out.println((count)+". Event Name : " + nameVar);
	         // System.out.println("Event ID : " + idVar);
	          System.out.println("Event URL : " + eventURLVar);
	          //System.out.println("Description : " + desVar);
	          System.out.println("Status of the event : " + statusVar);
	          System.out.println("Address of the event : " + addressVar );
	          System.out.println("City : " + cityVar);
	          WriteFile f = new WriteFile(count++,idVar,nameVar,eventURLVar,statusVar,addressVar,cityVar);
	          f.writeEvents();
	        		}
	    		}
	     	
		 } catch (Exception e) {
	         e.printStackTrace();
	      }
	         
	}
	
	void GetEvent(String event){
		 try {	
			 File inputFile = new File("input.txt");
	         DocumentBuilderFactory dbFactory 
	            = DocumentBuilderFactory.newInstance();
	         DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
	         Document doc = dBuilder.parse(inputFile);
	         doc.getDocumentElement().normalize();
	         NodeList nList = doc.getElementsByTagName("item");
	    	for (int temp = 0; temp < nList.getLength(); temp++) {

	    		Node nNode = nList.item(temp);
	    		//System.out.println("\nCurrent Element :" + nNode.getNodeName());
	    		//System.out.println(event);
	    		//System.out.println("\n");
	    		if (nNode.getNodeType() == Node.ELEMENT_NODE) {
	    			Element eElement = (Element) nNode;
	    			String idVar = eElement.getElementsByTagName("id").item(0).getTextContent();
	    			//System.out.println(idVar+event);
	    			if (idVar.equals(event))
	    			{
	    				//System.out.println(event);
		    			//System.out.println(idVar);
	    				String nameVar;
	    					if ( eElement.getElementsByTagName("name").item(0)  == null )
	    						nameVar = " "; 
	    					else 
	    						nameVar = eElement.getElementsByTagName("name").item(0).getTextContent();
	        	  
	    				String eventURLVar;
	    					if ( eElement.getElementsByTagName("event_url").item(0)  == null )
	    						eventURLVar = " "; 
	    					else 
	    						eventURLVar = eElement.getElementsByTagName("event_url").item(0).getTextContent();
	          
	    				String statusVar;
	        	  if ( eElement.getElementsByTagName("status").item(0)  == null )
	        		  statusVar = " "; 
	        	  else 
	        		  statusVar = eElement.getElementsByTagName("status").item(0).getTextContent();
	          
	        	  String addressVar;
	        	  if ( eElement.getElementsByTagName("address_1").item(0)  == null )
	        		  addressVar = " "; 
	        	  else 
	        		  addressVar = eElement.getElementsByTagName("address_1").item(0).getTextContent();
	          
	        	  String cityVar;
	        	  if ( eElement.getElementsByTagName("city").item(0)  == null )
	        		  cityVar = " "; 
	        	  else 
	        		  cityVar = eElement.getElementsByTagName("city").item(0).getTextContent();
	          
	          System.out.println("Event Name : " + nameVar);
	         // System.out.println("Event ID : " + idVar);
	          System.out.println("Event URL : " + eventURLVar);
	          //System.out.println("Description : " + desVar);
	          System.out.println("Status of the event : " + statusVar);
	          System.out.println("Address of the event : " + addressVar );
	          System.out.println("City : " + cityVar);
	          return;
	          		}
	        	}
	    	}
		}
		 catch (Exception e) {
	         e.printStackTrace();
	    }
	         
	}

	void XMLparseMember(String eventId){
		 try {	
			// m1.getOutput(URLgenerator.MEMBER.memberUrlGen(eventId));
			 String[] memberName = {"Kashfia", "Aditya","Tom","Sam","Ben","Danny","Reily","Emma","Bonny","Tucker"};
			 Member m1 = new Member(memberName);
			 WriteFile f = new WriteFile();
			 f.writeMembers(eventId,m1.SetMember());	        		
	    	 } 
	catch (Exception e) {
	         e.printStackTrace();
	   }
	         
	}	
}