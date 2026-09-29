using BlazorBootstrap;
namespace GestionPrestamoLibro.Extensors;

public static class ToastServiceExtensions
{
    public static ToastMessage ShowToast(this ToastService toastService, ToastType toastType, string title, string? customMessage = null)
    {
        ToastMessage message = new ToastMessage();
        message.Type = toastType;
        message.Title = title;

        if (customMessage == null)
        {
            message.Message = "A las " + DateTime.Now.ToString("hh:mm tt");
        }
        else
        {
            message.Message = customMessage;
        }

        toastService.Notify(message);
        return message;
    }

    public static ToastMessage ShowSuccess(this ToastService toastService, string? customMessage = null, string title = "Success")
    {
        return toastService.ShowToast(ToastType.Success, title, customMessage);
    }

    public static ToastMessage ShowWarning(this ToastService toastService, string? customMessage = null, string title = "Warning")
    {
        return toastService.ShowToast(ToastType.Warning, title, customMessage);
    }

    public static ToastMessage ShowError(this ToastService toastService, string? customMessage = null, string title = "Error")
    {
        return toastService.ShowToast(ToastType.Danger, title, customMessage);
    }
}
