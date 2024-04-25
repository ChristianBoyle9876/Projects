package com.community.connections;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.net.URL;

import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.http.ResponseEntity;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;

import com.community.connections.Users.User;
import com.fasterxml.jackson.databind.ObjectMapper;

@SpringBootTest
class UserTest {

	private TestRestTemplate restTemplate;
	private URL base;

	// @AfterEach
	// public void tearDown() {
	// String url = "http://localhost:8080/users?username=NMSavage10";
	// ResponseEntity<String> response = restTemplate.exchange(url,
	// HttpMethod.DELETE, null, String.class);
	// }

	@Test
	public void getUserResponse() throws Exception {
		restTemplate = new TestRestTemplate();
		base = new URL("http://localhost:8080/users");
		ResponseEntity<String> response = restTemplate.getForEntity(base.toString(), String.class);
		assertEquals(HttpStatus.OK, response.getStatusCode());
	}

	@Test
	public void getUserName() throws Exception {
		restTemplate = new TestRestTemplate();
		base = new URL("http://localhost:8080/users");
		ResponseEntity<String> response = restTemplate.getForEntity(base.toString(), String.class);
		ObjectMapper mapper = new ObjectMapper();
		String json = response.getBody();
		User[] users = mapper.readValue(json, User[].class);
		assertEquals("ColinD13", users[0].getUsername());
	}

	@Test
	public void testPostUser() throws Exception {
		restTemplate = new TestRestTemplate();
		base = new URL("http://localhost:8080/users");
		User user = new User();
		user.setUsername("NMSavage10");
		user.setPassword("password");
		user.setEmail("sdfsdf");
		user.setFirstName("sdfsdf");
		user.setLastName("sdfsdf");
		user.setPhoneNum("sdfsdf");
		user.setAuthQuestion("sdfsdf");
		user.setAuthAnswer("sdfsdf");

		ResponseEntity<String> response = restTemplate.postForEntity(base.toString(), user, String.class);

	}
}