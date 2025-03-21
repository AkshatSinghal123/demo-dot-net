import requests
from selenium import webdriver
from selenium.webdriver.common.by import By
from bs4 import BeautifulSoup
from webdriver_manager.chrome import ChromeDriverManager
import time
import os

# Path to your Razor Index.cshtml file (relative to repo root)
CODE_PATH = os.path.join("Pages", "Index.cshtml")
APP_URL = "https://kcc-vm-e5csgrcjgqgzazfn.australiaeast-01.azurewebsites.net/"  # 👈 Replace with your actual deployed URL

# ✅ Step 1: Check if URL is reachable (200 OK)
try:
    response = requests.get(APP_URL)
    if response.status_code == 200:
        print(f"✅ URL is working fine: {APP_URL} (Status: {response.status_code})")
    else:
        print(f"❌ URL returned an unexpected status: {response.status_code}")
        raise AssertionError(f"Test failed. URL did not return 200 OK. Got {response.status_code}")
except requests.exceptions.RequestException as e:
    print(f"❌ Failed to reach URL: {APP_URL}")
    raise AssertionError(f"Test failed. URL is unreachable. Error: {e}")

# ✅ Step 2: Read and parse the Razor source file
with open(CODE_PATH, "r", encoding="utf-8") as f:
    soup = BeautifulSoup(f.read(), "html.parser")

teams_in_code = [div.text.strip() for div in soup.select(".team-card")]
print("📦 Teams in Razor code:", teams_in_code)

# ✅ Step 3: Launch headless browser to fetch deployed UI
options = webdriver.ChromeOptions()
options.add_argument("--headless")
options.add_argument("--no-sandbox")
options.add_argument("--disable-dev-shm-usage")

driver = webdriver.Chrome(options=options)
driver.get(APP_URL)
time.sleep(3)

teams_in_ui = [el.text.strip() for el in driver.find_elements(By.CLASS_NAME, "team-card")]
print("🖥️ Teams in UI:", teams_in_ui)

# ✅ Step 4: Compare Razor code vs. UI output
missing = [team for team in teams_in_code if team not in teams_in_ui]

if not missing:
    print("✅ All teams from code are rendered in UI.")
else:
    print("❌ Missing team(s) in UI:", missing)
    raise AssertionError(f"Test failed. Missing team(s): {missing}")

# ✅ Step 5: Check if the new team added in Razor is visible in the UI
new_team = "New Team"  # Change this if your newly added team has a different name
if new_team in teams_in_ui:
    print(f"✅ New team '{new_team}' is listed on the website.")
else:
    print(f"❌ New team '{new_team}' is missing in the UI!")
    raise AssertionError(f"Test failed. New team '{new_team}' not found in UI.")

driver.quit()
