This is a JustCode extension that provides additional warnings and errors about formatting and styling

 Warnings provided by this extension are as follows:

----------

- Version 1.1.6
	- Checks that spacing is correct around the following keywords
		- new, for, foreach, in, switch, catch

**Note:** unfortunately due to the JustCode Extension Api, when a warning is detected it will highlight the entire code block.  If in the Future Telerik allows me to highlight only the character(s) causing the warning, I will fix it.

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