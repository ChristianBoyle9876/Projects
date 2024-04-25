package com.community.connections.Users;

import org.springframework.web.bind.annotation.RestController;

import java.util.*;

import org.springframework.http.*;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;

import java.sql.*;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestParam;

@RestController
public class UsersController {
    String connectionString = "your connection string here";

    @GetMapping("/users")
    public ResponseEntity<List<User>> getUsers() {
        List<User> users = new ArrayList<User>();
        User current = new User();

        try {
            Connection con = DriverManager.getConnection(connectionString);
            String sql;
            ResultSet rs;

            Statement stmt = con.createStatement();
            sql = "select * from [User]";
            rs = stmt.executeQuery(sql);

            while (rs.next()) {
                current = new User();
                current.setUsername(rs.getString("username"));
                current.setPassword(rs.getString("password"));
                current.setEmail(rs.getString("email"));
                current.setFirstName(rs.getString("firstName"));
                current.setLastName(rs.getString("lastName"));
                current.setPhoneNum(rs.getString("phoneNumber"));
                current.setAuthQuestion(rs.getString("authQuestion"));
                current.setAuthAnswer(rs.getString("authAnswer"));
                users.add(current);
            }

            con.close();

        } catch (Exception e) {
            System.out.println(e.getMessage());

            return new ResponseEntity<List<User>>(users, HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity<List<User>>(users, HttpStatus.OK);
    }

    @PostMapping("/users")
    public ResponseEntity<String> postUser(@RequestBody User newUser) {

        String added = "";

        // check if any fields are missing
        if (newUser.getFirstName() == null || newUser.getLastName() == null || newUser.getUsername() == null
                || newUser.getEmail() == null || newUser.getPassword() == null || newUser.getPhoneNum() == null
                || newUser.getAuthAnswer() == null || newUser.getAuthQuestion() == null) {

            added = "Missing Field: User Cannot be added";
            return new ResponseEntity<String>(added, HttpStatus.BAD_REQUEST);

        }

        try {
            Connection conn = DriverManager.getConnection(connectionString);
            String insertSQL;

            String checkSQL = "select * from [User] where username = ?";
            PreparedStatement checkStmt = conn.prepareStatement(checkSQL);
            checkStmt.setString(1, newUser.getUsername());
            ResultSet rs = checkStmt.executeQuery();

            // if the username is not already in use
            if (!rs.next()) {
                Statement insertStmt = conn.createStatement();
                insertSQL = String.format("insert into [User] values ('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s');",
                        newUser.getUsername(), newUser.getPassword(), newUser.getEmail(), newUser.getFirstName(),
                        newUser.getLastName(), newUser.getPhoneNum(), newUser.getAuthQuestion(),
                        newUser.getAuthAnswer());
                insertStmt.executeUpdate(insertSQL);
                added = "User was added successfully";
                return new ResponseEntity<String>(added, HttpStatus.OK);
            } else {
                added = "Username is already in use";
                return new ResponseEntity<String>(added, HttpStatus.BAD_REQUEST);
            }

        } catch (Exception e) {
            added = "Error adding user";
            return new ResponseEntity<String>(added, HttpStatus.BAD_REQUEST);
        }
    }

    @DeleteMapping("/users")
    public ResponseEntity<String> deleteUser(@RequestParam(value = "username", defaultValue = "") String username) {

        String deleted = "";

        // check if there is a username in the call
        if (username == null) {
            deleted = "No Username was specified";
            return new ResponseEntity<String>(deleted, HttpStatus.BAD_REQUEST);
        } else {

            String deleteSQL = "delete from [User] where username = ?";

            try {

                Connection con = DriverManager.getConnection(connectionString);
                PreparedStatement stmt = con.prepareStatement(deleteSQL);
                stmt.setString(1, username);
                int rowsAffected = stmt.executeUpdate();

                if (rowsAffected > 0) {
                    deleted = "User was deleted successfully";
                    return new ResponseEntity<String>(deleted, HttpStatus.OK);
                } else {
                    deleted = "No User was found with that username";
                    return new ResponseEntity<String>(deleted, HttpStatus.NOT_FOUND);
                }

            } catch (Exception e) {
                deleted = "Error deleting user";
                return new ResponseEntity<String>(deleted, HttpStatus.BAD_REQUEST);
            }
        }
    }

    @PutMapping("/users")
    public ResponseEntity<String> updateUser(@RequestBody User updatedUser) {

        String updated = "";

        try {
            Connection conn = DriverManager.getConnection(connectionString);
            String updateSQL;

            // check if user exists
            String checkSQL = "SELECT * FROM [User] WHERE username = ?";
            PreparedStatement checkStmt = conn.prepareStatement(checkSQL);
            checkStmt.setString(1, updatedUser.getUsername());
            ResultSet rs = checkStmt.executeQuery();

            if (rs.next()) {
                // if exists, update
                updateSQL = "UPDATE [User] SET password=?, email=?, firstName=?, lastName=?, phoneNumber=?, authQuestion=?, authAnswer=? WHERE username=?";
                PreparedStatement updateStmt = conn.prepareStatement(updateSQL);
                updateStmt.setString(1, updatedUser.getPassword());
                updateStmt.setString(2, updatedUser.getEmail());
                updateStmt.setString(3, updatedUser.getFirstName());
                updateStmt.setString(4, updatedUser.getLastName());
                updateStmt.setString(5, updatedUser.getPhoneNum());
                updateStmt.setString(6, updatedUser.getAuthQuestion());
                updateStmt.setString(7, updatedUser.getAuthAnswer());
                updateStmt.setString(8, updatedUser.getUsername());
                int rowsAffected = updateStmt.executeUpdate();

                if (rowsAffected > 0) {
                    updated = "User was updated successfully";
                    return new ResponseEntity<String>(updated, HttpStatus.OK);
                } else {
                    updated = "Failed to update user";
                    return new ResponseEntity<String>(updated, HttpStatus.BAD_REQUEST);
                }
            } else {
                updated = "User not found";
                return new ResponseEntity<String>(updated, HttpStatus.NOT_FOUND);
            }
        } catch (SQLException e) {
            updated = "Error updating user";
            return new ResponseEntity<String>(updated, HttpStatus.BAD_REQUEST);
        }
    }

}
