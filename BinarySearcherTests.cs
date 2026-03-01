namespace Testing;

/// <summary>
/// MC/DC tests for BinarySearcher.Search
/// Decision: while (bottom <= top && found == false)
///   Condition A: bottom <= top
///   Condition B: found == false
///
/// MC/DC truth table:
///   T1: A=T, B=T → Decision=T  (baseline – loop enters)
///   T2: A=F, B=T → Decision=F  (toggles A)
///   T3: A=T, B=F → Decision=F  (toggles B)
/// </summary>
[TestFixture]
public class BinarySearcherTests
{
    private BinarySearcher _searcher;

    [SetUp]
    public void Setup()
    {
        _searcher = new BinarySearcher();
    }

    // ── T1 + T3: Loop enters (T,T), key found → exits via found=true (T,F) ──

    [Test]
    public void Search_KeyFoundInMiddle_ReturnsCorrectIndex()
    {
        var orders = new[]
        {
            new Order(10, "Alice"),
            new Order(20, "Bob"),
            new Order(30, "Charlie"),
            new Order(40, "Dave"),
            new Order(50, "Eve")
        };

        int result = _searcher.Search(30, orders);

        Assert.That(result, Is.EqualTo(2));
    }

    // ── T2: Key not present, smaller than all → bottom > top while found==false (F,T) ──

    [Test]
    public void Search_KeySmallerThanAll_ReturnsNegativeOne()
    {
        var orders = new[]
        {
            new Order(10, "Alice"),
            new Order(20, "Bob"),
            new Order(30, "Charlie")
        };

        int result = _searcher.Search(5, orders);

        Assert.That(result, Is.EqualTo(-1));
    }

    // ── T2 variant: Key not present, larger than all → bottom > top (F,T) ──

    [Test]
    public void Search_KeyLargerThanAll_ReturnsNegativeOne()
    {
        var orders = new[]
        {
            new Order(10, "Alice"),
            new Order(20, "Bob"),
            new Order(30, "Charlie")
        };

        int result = _searcher.Search(99, orders);

        Assert.That(result, Is.EqualTo(-1));
    }

    // ── T1 + T3: Single element, key matches → loop enters once (T,T) then exits (T,F) ──

    [Test]
    public void Search_SingleElementFound_ReturnsZero()
    {
        var orders = new[] { new Order(42, "Solo") };

        int result = _searcher.Search(42, orders);

        Assert.That(result, Is.EqualTo(0));
    }

    // ── T2: Empty array → loop never entered, bottom > top immediately (F,T) ──

    [Test]
    public void Search_EmptyArray_ReturnsNegativeOne()
    {
        var orders = Array.Empty<Order>();

        int result = _searcher.Search(1, orders);

        Assert.That(result, Is.EqualTo(-1));
    }
}
