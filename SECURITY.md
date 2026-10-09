# Security Policy

## Supported versions

| Version | Supported |
| --- | --- |
| 0.1.1 | Yes |
| 0.1.0 | No (builds SQL from user input; upgrade to 0.1.1) |

## Reporting a vulnerability

Please **do not** open a public issue for security problems.

Report it privately through GitHub: go to the repository's **Security** tab and click **Report a vulnerability** ([direct link](https://github.com/nncast/vb.net-pharmacy-inventory/security/advisories/new)).

Include:

- the version and whether you used the Windows build or built from source
- the steps to reproduce, and which screen or file is affected
- what an attacker could do with it (for example change stock levels, alter orders or deliveries, read customer and supplier records)

You should get a reply within 7 days. Once the problem is confirmed, a fix is released as a new version and you are credited in the release notes unless you prefer not to be.

## Deployment notes

Pharmease is a desktop app that talks directly to MySQL, and it has no sign-in screen: anyone who can open it can change products, stock, orders and deliveries.

- Install it only on computers used by pharmacy staff, and lock them when nobody is at the counter.
- Do not use the MySQL `root` account with an empty password outside a local test machine. Create a dedicated MySQL user with access only to `dbpharmacy` and put it in the `PharmacyDb` connection string.
- Do not expose the MySQL port (3306) to the internet. Anyone who can reach the database with the credentials in `PharmacyInventory.exe.config` can read and change all records.
- Keep `PharmacyInventory.exe.config` readable only by the people who run the app, since it holds the database password.
