import static org.junit.Assert.assertTrue;

import java.time.Duration;

import org.junit.*;
import org.openqa.selenium.*;
import org.openqa.selenium.chrome.*;
import org.openqa.selenium.support.ui.*;

public class WebsiteTesting {

    WebDriver driver;

    @Before
    public void setup() {
        ChromeOptions options = new ChromeOptions();
        options.addArguments("--remote-allow-orgins=*");
        System.setProperty("webdriver.chrome.driver",
                "C:\\SeleniumDrivers\\chromedriver-win64\\chromedriver-win64\\chromedriver.exe");
        driver = new ChromeDriver(options);

    }

    @Test
    public void Test_Login() {

        driver.get("https://localhost:7244/");

        WebElement usernameEntry = driver.findElement(By.id("selectedUsername"));
        usernameEntry.sendKeys("ColinDowning");

        WebElement passwordEntry = driver.findElement(By.id("password"));
        passwordEntry.sendKeys("abcde");

        WebElement submit = driver.findElement(By.xpath("/html/body/div[1]/main/div/div/div/form/div[3]/input"));
        submit.click();

        WebElement alertTitleNextPage = driver.findElement(By.xpath("/html/body/div/main/div/div/h1"));
        assertTrue(alertTitleNextPage.isEnabled());
    }

    @Test
    public void Test_Register() {
        driver.get("https://localhost:7244/Users/Create");

        WebElement username = driver.findElement(By.id("UserName"));
        username.sendKeys("New Person3");

        WebElement password = driver.findElement(By.id("Password"));
        password.sendKeys("testing");

        WebElement email = driver.findElement(By.id("Email"));
        email.sendKeys("sdfsdfd@gmail.com");

        WebElement firstName = driver.findElement(By.id("FirstName"));
        firstName.sendKeys("Joe");

        WebElement lastName = driver.findElement(By.id("LastName"));
        lastName.sendKeys("Ball");

        WebElement phone = driver.findElement(By.id("PhoneNumber"));
        phone.sendKeys("45545453553");

        WebElement authQuestion = driver.findElement(By.id("AuthQuestion"));
        Select auth = new Select(authQuestion);
        auth.selectByIndex(1);

        WebElement authAnswer = driver.findElement(By.id("AuthAnswer"));
        authAnswer.sendKeys("Blue");

        WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
        WebElement submit = wait.until(ExpectedConditions.elementToBeClickable(By.cssSelector("input[type='submit']")));
        ((JavascriptExecutor) driver).executeScript("arguments[0].click();", submit);
        // submit.click();

        WebElement nextPage = driver.findElement(By.xpath("/html/body/div[1]/main/div/div/h1"));
        assertTrue(nextPage.isEnabled());
    }

    @Test
    public void Post_Alert() {
        driver.get("https://localhost:7244/Alerts/Create");

        WebElement alertTitle = driver.findElement(By.id("AlertTitle"));
        alertTitle.sendKeys("Testing");

        WebElement alertType = driver.findElement(By.id("AlertType"));
        Select type = new Select(alertType);
        type.selectByIndex(1);

        WebElement alertStatus = driver.findElement(By.id("Status"));
        Select status = new Select(alertStatus);
        status.selectByIndex(1);

        WebElement location = driver.findElement(By.id("Location"));
        location.sendKeys("Oakdale");

        WebElement zipcode = driver.findElement(By.id("Zipcode"));
        zipcode.sendKeys("12345");

        WebElement alertDescription = driver.findElement(By.id("AlertDescription"));
        alertDescription.sendKeys("sdjfksjdfnskdjfnsdkjfndkfjndkf");

        WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
        WebElement submit = wait.until(ExpectedConditions.elementToBeClickable(By.cssSelector("input[type='submit']")));
        ((JavascriptExecutor) driver).executeScript("arguments[0].click();", submit);

        WebElement nextPage = driver.findElement(By.xpath("/html/body/div[1]/main/div/div/h1"));
        assertTrue(nextPage.isEnabled());
    }

    @Test
    public void Test_View_Profile() {
        driver.get("https://localhost:7244/");

        WebElement usernameEntry = driver.findElement(By.id("selectedUsername"));
        usernameEntry.sendKeys("ColinDowning");

        WebElement passwordEntry = driver.findElement(By.id("password"));
        passwordEntry.sendKeys("abcde");

        WebElement submit = driver.findElement(By.xpath("/html/body/div[1]/main/div/div/div/form/div[3]/input"));
        submit.click();

        WebElement profile = driver.findElement(By.xpath("/html/body/header/nav/div/div/ul[2]/li/a"));
        profile.click();

        WebElement answer = driver.findElement(By.xpath("//*[@id=\"authAnswer\"]"));
        answer.sendKeys("Enders Game");

        WebElement submitAnser = driver.findElement(By.xpath("/html/body/div/main/div/div/div/form/div[3]/input"));
        submitAnser.click();

        WebElement headerNextPage = driver.findElement(By.xpath("/html/body/div/main/div[1]/div/div/h1"));
        assertTrue(headerNextPage.isEnabled());
    }

    @Test
    public void Test_Sort_Zip() {
        driver.get("https://localhost:7244/Alerts");

        WebElement zipEntry = driver.findElement(By.xpath("/html/body/div/main/div/div/form/div[2]/div/div/input"));
        zipEntry.sendKeys("15102");

        WebElement zipSubmit = driver.findElement(By.xpath("/html/body/div/main/div/div/form/div[2]/div/div/button"));
        zipSubmit.click();

        WebElement shownZip = driver.findElement(By.xpath("/html/body/div/main/div/div/p[3]"));
        assertTrue(shownZip.isEnabled());
    }

    @Test
    public void Test_Zip_Shows_Weather() {
        driver.get("https://localhost:7244/Alerts");

        WebElement zipEntry = driver.findElement(By.xpath("/html/body/div/main/div/div/form/div[2]/div/div/input"));
        zipEntry.sendKeys("15102");

        WebElement zipSubmit = driver.findElement(By.xpath("/html/body/div/main/div/div/form/div[2]/div/div/button"));
        zipSubmit.click();

        WebElement shownTemp = driver.findElement(By.xpath("/html/body/div/main/div/div/p[2]"));
        assertTrue(shownTemp.isEnabled());
    }

}