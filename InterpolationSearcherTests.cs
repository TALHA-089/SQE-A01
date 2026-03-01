namespace Testing;

/// <summary>
/// MC/DC tests for InterpolationSearcher.Search
/// Decision: while (bottom <= top && key >= orders[bottom].OrderID && key <= orders[top].OrderID)
///   Condition A: bottom <= top
///   Condition B: key >= orders[bottom].OrderID
///   Condition C: key <= orders[top].OrderID
///
/// MC/DC truth table (3 conditions → N+1 = 4 minimum tests):
///   T1: A=T, B=T, C=T → Decision=T  (baseline – loop enters)
///   T2: A=F, B=T, C=T → Decision=F  (toggles A)
///   T3: A=T, B=F, C=T → Decision=F  (toggles B)
///   T4: A=T, B=T, C=F → Decision=F  (toggles C)
/// </summary>
[TestFixture]
public class InterpolationSearcherTests
{
    private InterpolationSearcher _searcher;

    [SetUp]
    public void Setup()
    {
        _searcher = new InterpolationSearcher();
    }

    // ── T1: All conditions true → key found (baseline) ──

    [Test]
    public void Search_KeyFound_ReturnsCorrectIndex()
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

    // ── T3: key < orders[bottom].OrderID → B=F, toggles B ──

    [Test]
    public void Search_KeyTooSmall_ReturnsNegativeOne()
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

    // ── T4: key > orders[top].OrderID → C=F, toggles C ──

    [Test]
    public void Search_KeyTooLarge_ReturnsNegativeOne()
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

    // ── T2: Empty array → bottom > top, A=F, toggles A ──

    [Test]
    public void Search_EmptyArray_ReturnsNegativeOne()
    {
        var orders = Array.Empty<Order>();

        int result = _searcher.Search(1, orders);

        Assert.That(result, Is.EqualTo(-1));
    }

    // ── Boundary: Single element, key matches ──

    [Test]
    public void Search_SingleElementFound_ReturnsZero()
    {
        var orders = new[] { new Order(42, "Solo") };

        int result = _searcher.Search(42, orders);

        Assert.That(result, Is.EqualTo(0));
    }

    // ── Boundary: Single element, key does not match ──

    [Test]
    public void Search_SingleElementNotFound_ReturnsNegativeOne()
    {
        var orders = new[] { new Order(42, "Solo") };

        int result = _searcher.Search(99, orders);

        Assert.That(result, Is.EqualTo(-1));
    }
}
