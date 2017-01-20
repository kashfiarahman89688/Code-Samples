package meetup;

public enum URLgenerator {
EVENT(MeetUpLive.api_host+"/2/open_events?format=xml&sign=true&photo-host=public&key="+MeetUpLive.api_key),
MEMBER(MeetUpLive.api_host+"/2/members?format=xml&sign=true&photo-host=public&key="+MeetUpLive.api_key);

		private final String urlToPass;  
	    
	    URLgenerator(String url) {
	        this.urlToPass = url;
	    }
	    
	    String eventUrlGen(String zip,String start, String end){
	    	return EVENT.urlToPass+"&zip="+zip.toString()+"&time="+start.toString()+","+end.toString();
	    	
	    }
	    
	    String memberUrlGen(String idEvent)
	    {
	    	return MEMBER.urlToPass+"&group_id=23776943".toString();
	    }
	    
}



