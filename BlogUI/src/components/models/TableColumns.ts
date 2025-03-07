export class TableColumns {
    name: string;
    description: string;
    align: 'left' | 'right' | 'center' | 'justify'; // Use a union type for valid values

    constructor(name: string, description: string, align: 'left' | 'right' | 'center' | 'justify') {
        this.name = name;
        this.description = description;
        this.align = align;
    }
}
