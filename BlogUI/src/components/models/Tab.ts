export class Tab {
    action: string;
    text: string;
    active: boolean;
    disabled: boolean;

    constructor(action: string, text: string,active: boolean, disabled? : boolean) {
        this.action = action;
        this.text = text;
        this.active = active;
        this.disabled = disabled ? disabled : false;
    }
}