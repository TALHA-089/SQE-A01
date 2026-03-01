namespace Testing;

/// <summary>
/// MC/DC tests for LinearSearcher.Search
/// Decision: if (orders[i].OrderID == key)
///   Single condition → MC/DC = branch coverage (true + false)
/// </summary>
[TestFixture]
public class LinearSearcherTests
{
    private LinearSearcher _searcher;

    [SetUp]
    public void Setup()
    {
        _searcher = new LinearSearcher();
    }

    // ── Condition true: key found at first position ──

    [Test]
    public void Search_KeyFoundAtStart_ReturnsZero()
    {
        var orders = new[]
        {
            new Order(10, "Alice"),
            new Order(20, "Bob"),
            new Order(30, "Charlie")
        };

        int result = _searcher.Search(10, orders);

        Assert.That(result, Is.EqualTo(0));
    }

    // ── Condition true: key found in the middle ──

    [Test]
    public void Search_KeyFoundInMiddle_ReturnsCorrectIndex()
    {
        var orders = new[]
        {
            new Order(10, "Alice"),
            new Order(20, "Bob"),
            new Order(30, "Charlie")
        };

        int result = _searcher.Search(20, orders);

        Assert.That(result, Is.EqualTo(1));
    }

    // ── Condition true: key found at last position ──

    [Test]
    public void Search_KeyFoundAtEnd_ReturnsLastIndex()
    {
        var orders = new[]
        {
            new Order(10, "Alice"),
            new Order(20, "Bob"),
            new Order(30, "Charlie")
        };

        int result = _searcher.Search(30, orders);

        Assert.That(result, Is.EqualTo(2));
    }

    // ── Condition always false: key not found ──

    [Test]
    public void Search_KeyNotFound_ReturnsNegativeOne()
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

    // ── Edge case: empty array ──

    [Test]
    public void Search_EmptyArray_ReturnsNegativeOne()
    {
        var orders = Array.Empty<Order>();

        int result = _searcher.Search(1, orders);

        Assert.That(result, Is.EqualTo(-1));
    }
}
