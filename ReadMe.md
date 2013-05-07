This is a Telerik JustCode extension that provides additional warnings and errors about formatting and styling

----------
- Version 1.2.0
	- Enhancements on rules engine to streamline checks
	- Fixed bug in JavaScript comments rules
	- Changed structure of extension to enhance the ability to conduct unit test.

Note: Leaving a project that using VB.NET so besides a few test I will have limited viability. Any errors or enhancement request can be sent to me. 

----------

- Version 1.1.8
	- Fixed small bug in reworked space checker engine

----------

- Version 1.1.7
	- Improved warning messages, this should improve speed of locating keyword spacing warnings.
	- removed stock sample code

----------

- Version 1.1.6
	- Improved the rule for comment spacing
	- Checks that spacing is correct around the following keywords
		- new, for, foreach, in, switch, catch

**Note:** unfortunately due to the JustCode Extension Api, when a warning is detected it will highlight the entire code block.  *If in the Future Telerik allows me to highlight only the character(s) causing the warning, I will fix it.*

----------

- Version 1.1.5.1
	- Added a license file (sorry)

----------

- Version 1.1.5
	- Adds two rules from StyleCop
		- SA1200: A C# using directive must be placed within the Namespace
			- Placing a using directive within the namespace eliminates compiler confusion that can happen between conflicting types.
			- Enhanced scope reference and aliases
		- SA1005: C# Single line commens must begin with a single space