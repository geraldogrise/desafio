class DateUtil {

    static format = (date: Date , isToday: boolean = false): string => {
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed
        const year = date.getFullYear();
        const today = new Date();
        const normalizedDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
        const normalizedToday = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        
        if (normalizedDate.getTime() === normalizedToday.getTime()) {
            return `${year}-${month}-${day}`;
        }
        return `${year}-${month}-${Number(day)+1}`;
    }

    static formatToBR = (date: Date | null , isToday: boolean = false): string => {
        if(date == null)
        {
            return "";
        }
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed
        const year = date.getFullYear();
        const today = new Date();
        const normalizedDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
        const normalizedToday = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        
        if (normalizedDate.getTime() === normalizedToday.getTime()) {
            return `${day}/${month}/${year}`;
        }
        return `${day}/${month}/${year}`;
    }

     static formatDate = (date?: Date | null | string): string => {
        if(date == null)
        {
            return "";
        }
        if (typeof date === 'string') {
           const parsedDate = new Date(date);
           return  this.format(parsedDate);
        }
        if (date) {
            return this.format(date);
        } else {
            return this.format(new Date());
        }
    };

    static isValidDate = (date: any): boolean => {
        return date instanceof Date && !isNaN(date.getTime());
    };
}

export default DateUtil;
