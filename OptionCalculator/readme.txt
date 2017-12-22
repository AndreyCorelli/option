1. Press the "..." button and pick your file with a source timeseries.
   The file should contain data like this:

2015.12.01 00:00,1000.00,1000.00,994.40,994.40,1440
2015.12.01 00:00,994.40,995.34,994.40,995.34,1440
...
   or this:
994.40
995.34
...

2. Fill the option contract parameters: current active's price (current), strike price (strike),
   term in days (term, can be a fractional number), volume (count of contracts to buy - CALL or sell - PUT).
   Pick the option type - CALL or PUT.
   Check the "detrend" box if you want to remove trend from the source data (recommended).
   Provide the desired iterations count. I recommend choosing the value in 10000 - 1000000 range.

3. Press "Calculate premium". The result will be in the "premium" field.
   Check the program folders for two files: distribution.txt and profits.png.