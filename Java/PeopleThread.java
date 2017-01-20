package People;

import java.util.Scanner;

public class PeopleThread extends Thread {
		private static int choice = 3; 
		private static int previuosChoice = 4;
		private volatile boolean running = false;
		public void terminate() { running = false; }
		public void prepare() { running = true; }
		People p;
		
		public PeopleThread(People p)
		  { super();  this.p = p;
		  
		  }

		public void run()
		  { 
			while(true){
			while(running){ 
			p.print();
		     try {
				Thread.sleep(1000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
			}  
			}
		}
			
			
				public static void main(String[] args){
				Scanner in = new Scanner(System.in);
				People p1 = new People("Person 1");
				People p2 = new People("Person 2");
				
				PeopleThread pt1 = new PeopleThread(p1);
				PeopleThread pt2 = new PeopleThread(p2);
				pt1.start();
				pt2.start();
				
				System.out.println("Please enter your choice : ");
				System.out.println("1. Exit the main program.");
				System.out.println("2. Start printing.");
				System.out.println("3. Stop.");
				
				while ( choice != 1 ){
					System.out.printf("Please choose your option: ");
					choice = in.nextInt();
					//System.out.println();
					
					if (choice == 1){
						System.out.println("Exit");
						System.exit(0);
					}
						
					else if (choice == 2 && choice != previuosChoice){
						//System.out.println("Starting the two treads to print the two people's names."
						//		+ " This will continue to run until the choice 3 is selected.");
						pt1.prepare();
						pt2.prepare();
						
					} else if (choice == 3 && choice != previuosChoice){
						System.out.println("Stopping the threads!");
						pt1.terminate();    
						pt2.terminate();
					} else if (choice == previuosChoice)
						System.out.println("You have repeated your choice. Please try again.");
					
					else {
						System.out.println("Invalid choice! Please try again.");
					}
					System.out.println();
					previuosChoice = choice;

				}
				in.close();
				
				
			
		  
			
	}

}
