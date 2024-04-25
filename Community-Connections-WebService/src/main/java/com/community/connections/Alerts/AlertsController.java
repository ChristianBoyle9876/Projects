package com.community.connections.Alerts;

import org.springframework.web.bind.annotation.RestController;

import java.util.*;

import org.springframework.http.*;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;

import java.sql.*;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestParam;

@RestController
public class AlertsController {
    String connectionString = "your connection string here";

    @GetMapping("/alerts")
    public ResponseEntity<List<Alert>> getAlerts() {
        List<Alert> alerts = new ArrayList<Alert>();
        Alert currentAlert = new Alert();

        try {

            Connection con = DriverManager.getConnection(connectionString);
            String sql;
            ResultSet rs;

            Statement stmt = con.createStatement();
            sql = "select * from [Alert]";
            rs = stmt.executeQuery(sql);

            while (rs.next()) {
                currentAlert = new Alert();
                currentAlert.setTimePosted(rs.getDate("timePosted"));
                currentAlert.setAlertType(rs.getString("alertType"));
                currentAlert.setAlertTitle(rs.getString("alertTitle"));
                currentAlert.setAlertDescription(rs.getString("alertDescription"));
                currentAlert.setZipcode(rs.getString("zipcode"));
                currentAlert.setLocation(rs.getString("location"));
                currentAlert.setStatus(rs.getString("status"));
                currentAlert.setUsername(rs.getString("username"));
                alerts.add(currentAlert);
            }

            con.close();

        } catch (Exception e) {
            System.out.println(e.getMessage());
            currentAlert = null;
            return new ResponseEntity<>(alerts, HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity<>(alerts, HttpStatus.OK);
    }

    @PostMapping("/alerts")
    public ResponseEntity<String> postAlert(@RequestBody Alert newAlert) {

        String added = "";

        // check if any fields are missing
        if (newAlert.getTimePosted() == null || newAlert.getAlertType() == null
                || newAlert.getAlertTitle() == null || newAlert.getAlertDescription() == null
                || newAlert.getZipcode() == null
                || newAlert.getLocation() == null || newAlert.getStatus() == null || newAlert.getUsername() == null) {

            added = "Missing Field: User Cannot be added";
            return new ResponseEntity<String>(added, HttpStatus.BAD_REQUEST);
        }

        try {
            Connection conn = DriverManager.getConnection(connectionString);
            String insertSQL;

            // String checkSQL = "select * from [Alert] where AlertID = ?";
            // PreparedStatement checkStmt = conn.prepareStatement(checkSQL);
            // checkStmt.setInt(1, newAlert.getAlertID());
            // ResultSet rs = checkStmt.executeQuery();

            Statement insertStmt = conn.createStatement();
            insertSQL = String.format(
                    "insert into [Alert] values ('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s');",
                    newAlert.getTimePosted(), newAlert.getAlertType(),
                    newAlert.getAlertTitle(),
                    newAlert.getAlertDescription(), newAlert.getZipcode(), newAlert.getLocation(),
                    newAlert.getStatus(), newAlert.getUsername());
            insertStmt.executeUpdate(insertSQL);
            added = "Alert Was Posted";
            return new ResponseEntity<String>(added, HttpStatus.OK);

        } catch (Exception e) {
            System.out.println(e.getMessage() + e.getStackTrace());
            added = "Error posting Alert";
            return new ResponseEntity<String>(added, HttpStatus.BAD_REQUEST);

        }
    }

    @DeleteMapping("/alerts")
    public ResponseEntity<String> deleteAlert(@RequestParam(value = "alertID", defaultValue = "0") int alertID) {

        String deleted = "";

        // check if there is a username in the call
        if (alertID == 0) {
            deleted = "AlertID was specified";
            return new ResponseEntity<String>(deleted, HttpStatus.BAD_REQUEST);
        } else {

            String deleteSQL = "delete from [Alert] where alertID = ?";

            try {

                Connection con = DriverManager.getConnection(connectionString);
                PreparedStatement stmt = con.prepareStatement(deleteSQL);
                stmt.setInt(1, alertID);
                int rowsAffected = stmt.executeUpdate();

                if (rowsAffected > 0) {
                    deleted = "Alert was deleted successfully";
                    return new ResponseEntity<String>(deleted, HttpStatus.OK);
                } else {
                    deleted = "No Alert was found with that alertID";
                    return new ResponseEntity<String>(deleted, HttpStatus.NOT_FOUND);
                }

            } catch (Exception e) {
                deleted = "Error deleting alert";
                return new ResponseEntity<String>(deleted, HttpStatus.BAD_REQUEST);
            }
        }
    }

}
