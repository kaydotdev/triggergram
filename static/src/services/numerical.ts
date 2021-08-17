/**
 * Summary. Transforms positive number to a shorten decimal unit.
 * Description. Examples:
 * 
 *  1 -> "1"
 *  234 -> "234"
 *  1000 -> "1k"
 *  2345 -> "2k"
 *  1000000 -> "1M"
 * 
 * @param {number} num Positive number.
 * @returns {string} Shorten decimal unit.
 */
export function decimalMetric(num: number): string {
    if (num < 0)
        throw new Error("Only positive number allowed.");
    else if (num === 0)
        return "0";
    else {
        const decimals = Math.floor(Math.log10(num));
        const base = Math.floor(decimals / (10 ** decimals));

        if (decimals > 6)
            return `${base}M`
        else if (decimals > 3)
            return `${base}k`
        else
            return num.toString()
    }
}
