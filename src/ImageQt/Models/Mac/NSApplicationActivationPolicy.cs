namespace ImageQt.Models.Mac;

[Flags]
public enum NSApplicationActivationPolicy : ulong
{
    NSApplicationActivationPolicyRegular = 0,
    NSApplicationActivationPolicyAccessory = 1,
    NSApplicationActivationPolicyERROR = 2,
}
