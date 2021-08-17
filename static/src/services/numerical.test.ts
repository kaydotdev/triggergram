import { decimalMetric } from "./numerical";


describe('Edge cases', () => {
    it('should throw error if number is negative', () => {
        expect(() => decimalMetric(-9999)).toThrow(RangeError);
    });

    it('should return "0" if number is 0', () => {
        expect(decimalMetric(0)).toEqual("0");
    });
});

describe('Common cases', () => {
    const testCases = [
        { input: 1, expected: "1" },
        { input: 234, expected: "234" },
        { input: 999, expected: "999" },
        { input: 1000, expected: "1k" },
        { input: 1534, expected: "1k" },
        { input: 2345, expected: "2k" },
        { input: 21345, expected: "21k" },
        { input: 1000000, expected: "1M" },
        { input: 1250000, expected: "1M" },
        { input: 15250000, expected: "15M" }
    ];

    testCases.forEach(test => {
        it(`returns "${test.expected}" if number is ${test.input}`, () => 
            expect(decimalMetric(test.input)).toEqual(test.expected)
        );
    });
});
