public class Controller {

    private int min_range;
    private int max_range;

    private int secretValue;


    public Controller(int min, int max) {
        min_range = min;
        max_range = max;

        secretValue = (int)Math.ceil(Math.random() * (max_range - min_range - 1) + min_range);
    }


    public boolean checkValue (int value){
        if (value == secretValue){
            return false;
        } else if (value > secretValue){
            max_range = value;
        } else {
            min_range = value;
        }
        return true;
    }

    public int getMin_range() {
        return min_range;
    }
    public int getMax_range() {
        return max_range;
    }
}
