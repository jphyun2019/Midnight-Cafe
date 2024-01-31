import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class Story {
    private static Scanner userIn = new Scanner(System.in);
    public static void main(String[] args) throws FileNotFoundException {
        File f = new File("chapters/chapter1.txt");
        Scanner scan = new Scanner(f);
        while(scan.hasNextLine()){
            String currentLine = scan.nextLine();
            if(currentLine.equals("")) {
                System.out.println();
                continue; //makes some space
            }
            if(currentLine.equals("#") && scan.hasNextLine()){ //begins an option sequence
                optionSequence(scan); //enters the options sequence
                continue;
            }
            System.out.println(currentLine);
        }
        scan.close();
        userIn.close();
    }

    private static void optionSequence(Scanner scan) {
        int input = 0;
        boolean starSeen = false;
        boolean outputComplete = false;
        while(scan.hasNextLine()){
            String current = scan.nextLine();
            if(current.equals("#")) return; //end of option sequence
            if(!current.equals("*") && outputComplete==false){ //prints options
                System.out.println(current);
                continue;
            }
            if(current.equals("*") && starSeen == false){ //first time I see a star
                System.out.println("Please input your response as the option number");
                input = userIn.nextInt(); //Option decision number
                if(input == 1){
                    input--; //if they put a 1, they should have a zero
                } //if they put a 2, the value should stay as 2 (should skip starting and ending star of selection)
                starSeen = true;
            }
            if(current.equals("*") && starSeen == true && input == 0 && outputComplete == false){ //I just saw the starting star & this is the right star to finish off on
                //System.out.println("Option decision consequences:\n");
                String currentDecisionLines = scan.nextLine();
                while(!currentDecisionLines.equals("*")){ //we are the correction option block
                    System.out.println(currentDecisionLines);
                    currentDecisionLines = scan.nextLine();
                }
                input = -1;
                outputComplete = true;
                //System.out.println("\nFinished with reponse\n");
            } else if(input > 0) { //skip lines until we see another star
                String s = current; //should currently have a star
                while(input > 0){ //EX: 2 >0
                    //System.out.println("input is now: "+ input);
                    if(s.equals("*")) input--; //found a star input = 1;  Found another? input =0;
                    s = scan.nextLine();
                }
                //System.out.println("input is now: "+input);
                //System.out.println("The last thing we held onto is: " + s);
                s = scan.nextLine();
                while(!s.equals("*")){ //we are the correction option block
                    System.out.println(s);
                    s = scan.nextLine();
                }
                outputComplete =  true;
                //System.out.println("Done with option X");
            } else if(outputComplete == true){
                continue;//lowkey works
            }
        }
    }
}
