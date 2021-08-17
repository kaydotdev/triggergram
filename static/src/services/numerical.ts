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
        throw new RangeError("Only positive number allowed.");
    else if (num === 0)
        return "0";
    else {
        const decimals = Math.floor(Math.log10(num));

        if (decimals >= 6)
            return `${Math.floor(num / (10 ** 6))}M`
        else if (decimals >= 3)
            return `${Math.floor(num / (10 ** 3))}k`
        else
            return num.toString()
    }
}
