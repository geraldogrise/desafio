export class Item {
    value: string | number;
    text: string;
    active: boolean;

    constructor(value: string | number, text: string, active: boolean) {
        this.value = value;
        this.text = text;
        this.active = active
    }
}