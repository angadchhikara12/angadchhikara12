import random
import re

# List of 7 motivational quotes (Entrepreneurs + Tech Legends)
quotes = [
    ("The people who are crazy enough to think they can change the world are the ones who do.", "Steve Jobs"),
    ("Whether you think you can or think you can't, you're right.", "Henry Ford"),
    ("Talk is cheap. Show me the code.", "Linus Torvalds"),
    ("Don’t wait. The time will never be just right.", "Napoleon Hill"),
    ("The only way to do great work is to love what you do.", "Steve Jobs"),
    ("Hard work beats talent when talent doesn’t work hard.", "Tim Notke"),
    ("First, solve the problem. Then, write the code.", "John Johnson"),
    ("The best way to predict the future is to create it.","Peter Drucker")
]

# Pick a random quote
quote, author = random.choice(quotes)

# Read README
with open("README.md", "r", encoding="utf-8") as f:
    readme = f.read()

# Replace the existing quote section
new_section = f"## 💡 ✨ Quote of the Day\n\n❝ {quote} ❞  \n— {author}"

# Replace old section or add if missing
if "## 💡 ✨ Quote of the Day" in readme:
    updated_readme = re.sub(
        r"## 💡 ✨ Quote of the Day[\s\S]*?(?=\n##|$)", new_section, readme
    )
else:
    updated_readme = readme + "\n\n" + new_section

# Write back
with open("README.md", "w", encoding="utf-8") as f:
    f.write(updated_readme)
