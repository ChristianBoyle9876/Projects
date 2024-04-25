package com.community.connections;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.net.URL;
import java.sql.Date;

import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.http.ResponseEntity;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;

import com.community.connections.Alerts.Alert;
import com.fasterxml.jackson.databind.ObjectMapper;

@SpringBootTest
public class AlertTest {

    private TestRestTemplate restTemplate;
    private URL base;

    @Test
    public void testAlertGettersAndSetters() {
        Alert alert = new Alert();
        // alert.setAlertID(1);
        Date timePosted = new Date(System.currentTimeMillis());
        alert.setTimePosted(timePosted);
        alert.setAlertType("Test");
        alert.setAlertTitle("Test");
        alert.setAlertDescription("Test");
        alert.setZipcode("12345");
        alert.setLocation("Test");
        alert.setStatus("Test");
        alert.setUsername("Test");
        // assertEquals(1, alert.getAlertID());
        assertEquals(timePosted, alert.getTimePosted());
        assertEquals("Test", alert.getAlertType());
        assertEquals("Test", alert.getAlertTitle());
        assertEquals("Test", alert.getAlertDescription());
        assertEquals("12345", alert.getZipcode());
        assertEquals("Test", alert.getLocation());
        assertEquals("Test", alert.getStatus());
        assertEquals("Test", alert.getUsername());
    }

    @Test
    public void testGetAlertResponse() throws Exception {
        restTemplate = new TestRestTemplate();
        base = new URL("http://localhost:8080/alerts");
        ResponseEntity<String> response = restTemplate.getForEntity(base.toString(), String.class);
        assertEquals(HttpStatus.OK, response.getStatusCode());
    }

    @Test
    public void testGetAlertTitle() throws Exception {
        restTemplate = new TestRestTemplate();
        base = new URL("http://localhost:8080/alerts");
        ResponseEntity<String> response = restTemplate.getForEntity(base.toString(), String.class);
        ObjectMapper mapper = new ObjectMapper();
        String json = response.getBody();
        Alert[] alerts = mapper.readValue(json, Alert[].class);
        assertEquals("Tree Down", alerts[0].getAlertTitle());
    }

    @Test
    public void testPostAlert() throws Exception {
        restTemplate = new TestRestTemplate();
        base = new URL("http://localhost:8080/alerts");
        Alert alert = new Alert();
        // alert.setAlertID(2);
        Date timePosted = new Date(System.currentTimeMillis());
        alert.setTimePosted(timePosted);
        alert.setAlertType("Test");
        alert.setAlertTitle("Test");
        alert.setAlertDescription("Test");
        alert.setZipcode("12345");
        alert.setLocation("Test");
        alert.setStatus("Current");
        alert.setUsername("ColinD13");

        ResponseEntity<String> response = restTemplate.postForEntity(base.toString(), alert, String.class);

        assertEquals("Alert Was Posted", response.getBody());
    }

    @Test
    public void testPostAlertWithExistingID() throws Exception {
        restTemplate = new TestRestTemplate();
        base = new URL("http://localhost:8080/alerts");
        Alert alert = new Alert();
        // alert.setAlertID(1);
        Date timePosted = new Date(System.currentTimeMillis());
        alert.setTimePosted(timePosted);
        alert.setAlertType("Test");
        alert.setAlertTitle("Test");
        alert.setAlertDescription("Test");
        alert.setZipcode("12345");
        alert.setLocation("Test");
        alert.setStatus("Test");
        alert.setUsername("ColinD13");
        ResponseEntity<String> response = restTemplate.postForEntity(base.toString(), alert, String.class);
        assertEquals("Alert ID is already in use", response.getBody());
    }

    @Test
    public void testPostAlertWithMissingField() throws Exception {
        restTemplate = new TestRestTemplate();
        base = new URL("http://localhost:8080/alerts");
        Alert alert = new Alert();
        // alert.setAlertID(2);
        Date timePosted = new Date(System.currentTimeMillis());
        alert.setTimePosted(timePosted);
        alert.setAlertType("Test");
        alert.setAlertTitle("Test");
        alert.setAlertDescription("Test");
        alert.setZipcode("12345");
        alert.setLocation("Test");
        alert.setStatus("Test");
        ResponseEntity<String> response = restTemplate.postForEntity(base.toString(), alert, String.class);
        assertEquals("Missing Field: User Cannot be added", response.getBody());
    }

    @Test
    public void testDeleteAlert() throws Exception {
        restTemplate = new TestRestTemplate();
        base = new URL("http://localhost:8080/alerts");
        Alert alert = new Alert();
        // alert.setAlertID(2);
        Date timePosted = new Date(System.currentTimeMillis());
        alert.setTimePosted(timePosted);
        alert.setAlertType("Test");
        alert.setAlertTitle("Test");
        alert.setAlertDescription("Test");
        alert.setZipcode("12345");
        alert.setLocation("Test");
        alert.setStatus("Current");
        alert.setUsername("ColinD13");

        ResponseEntity<String> response = restTemplate.postForEntity(base.toString(), alert, String.class);

        String url = "http://localhost:8080/alerts?alertID=2";
        ResponseEntity<String> deleteResponse = restTemplate.exchange(url, HttpMethod.DELETE, null, String.class);
    }
}
