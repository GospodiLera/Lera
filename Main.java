import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scn = new Scanner(System.in);

        Controller controller1 = new Controller(0, 100);

        while(controller1.checkValue(inputIntValueWithScanner(controller1, scn)));

        System.out.println("win");
    }

    private static int inputIntValueWithScanner(Controller cntr, Scanner scn) {
        int res = 0;
        System.out.println("Guess number from thia range [" + cntr.getMin_range() + " " + cntr.getMax_range() + "]");
        while (true) {
            while (!scn.hasNextInt()) {
                System.out.println("Wrong input");
                scn.next();
            }
            if ((res = scn.nextInt()) <= cntr.getMin_range() ||
                    res >= cntr.getMax_range()) {
                System.out.println("Wrong input");
                continue;
            }
            break;
        }
        return res;
    }
}
