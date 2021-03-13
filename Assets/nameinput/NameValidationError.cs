abstract class NameValidationError
{
    public int Pos { get; }

    protected NameValidationError(int pos)
    {
        Pos = pos;
    }

    public class IsBlank : NameValidationError
    {
        public IsBlank(int pos) : base(pos) { }
    }

    public class Clash : NameValidationError
    {
        public int WithPos { get; }

        public Clash(int pos, int withPos) : base(pos)
        {

            WithPos = withPos;
        }
    }
}
