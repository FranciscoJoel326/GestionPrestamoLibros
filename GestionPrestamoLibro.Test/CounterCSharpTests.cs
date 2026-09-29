namespace GestionPrestamoLibro.Test;

public class CounterCSharpTests : TestContext
{
    [Fact]
    public void ShouldRenderPrestamoPickerCorrectly()
    {
        var prestamos = new List<Prestamos>();

        Prestamos prestamo1 = new Prestamos();
        prestamo1.PrestamoId = 1;
        prestamo1.Concepto = "Concepto 1";
        prestamo1.Balance = 100.0;
        prestamos.Add(prestamo1);

        Prestamos prestamo2 = new Prestamos();
        prestamo2.PrestamoId = 2;
        prestamo2.Concepto = "Concepto 2";
        prestamo2.Balance = 200.0;
        prestamos.Add(prestamo2);

        var component = RenderComponent<PrestamoPicker>(parameters => parameters
            .Add(p => p.Prestamos, prestamos)
            .Add(p => p.PrestamoId, 0)
            .Add(p => p.Valor, 0.0)
        );

        var selectElement = component.Find("select");
        var inputElement = component.Find("input#quantity-input");
        var buttonElement = component.Find("button");

        Assert.NotNull(selectElement);
        Assert.NotNull(inputElement);
        Assert.NotNull(buttonElement);
    }
}
