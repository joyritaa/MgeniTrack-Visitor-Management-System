-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 01, 2026 at 12:24 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mgenitrack`
--

-- --------------------------------------------------------

--
-- Table structure for table `activity_logs`
--

CREATE TABLE `activity_logs` (
  `log_id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `action_type` varchar(100) DEFAULT NULL,
  `action_details` text DEFAULT NULL,
  `related_entity_type` varchar(50) DEFAULT NULL,
  `related_entity_id` int(11) DEFAULT NULL,
  `ip_address` varchar(50) DEFAULT NULL,
  `user_agent` varchar(255) DEFAULT NULL,
  `time_stamp` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `activity_logs`
--

INSERT INTO `activity_logs` (`log_id`, `user_id`, `action_type`, `action_details`, `related_entity_type`, `related_entity_id`, `ip_address`, `user_agent`, `time_stamp`) VALUES
(1, 12, 'Login', 'Brian logged in as Resident', 'User', 12, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:21:28'),
(2, 12, 'Login', 'Brian logged in as Resident', 'User', 12, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:21:39'),
(3, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:22:10'),
(4, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:22:51'),
(5, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:22:51'),
(6, 1, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:26:56'),
(7, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:26:59'),
(8, 14, 'CheckIn', 'Walk-in: Helena Mercy checked into C502 (BnB Stay)', 'Visit', 5, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:28:08'),
(9, 14, 'CheckOut', 'Chris Kirubi checked out from A101. Duration: 1337 mins', 'Visit', 2, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:28:33'),
(10, 14, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:29:23'),
(11, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:29:28'),
(12, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:31:16'),
(13, 10, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:33:30'),
(14, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-26 13:33:36'),
(15, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-28 13:46:20'),
(16, 1, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-28 13:46:41'),
(17, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0', '2026-03-28 13:46:46'),
(18, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 13:23:12'),
(19, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 13:23:12'),
(20, 4, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 13:26:28'),
(21, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 13:26:33'),
(22, 1, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 13:30:07'),
(23, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 13:30:14'),
(24, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 13:41:16'),
(25, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 15:20:59'),
(26, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 15:20:59'),
(27, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 15:23:05'),
(28, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 15:24:28'),
(29, 4, 'CheckIn', 'Walk-in: Shiko Rakel checked into C305 (BnB Stay)', 'Visit', 6, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-22 15:25:34'),
(30, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:02:04'),
(31, 4, 'CheckOut', 'Helena Mercy checked out from C502. Duration: 40235 min', 'Visit', 5, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:03:35'),
(32, 4, 'CheckOut', 'Shiko Rakel checked out from C305. Duration: 1238 min', 'Visit', 6, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:03:42'),
(33, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:46:17'),
(34, 15, 'Login', 'Wahu logged in as Resident', 'User', 15, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:50:50'),
(35, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:51:47'),
(36, 15, 'Login', 'Wahu logged in as Resident', 'User', 15, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:54:10'),
(37, 15, 'Create', 'Invitation created for Nameless Kagwe (token: 86EA77A135)', 'VisitorInvitation', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:54:51'),
(38, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:55:08'),
(39, 14, 'CheckIn', 'Pre-registered: Nameless Kagwe checked into B203 via invitation', 'Visit', 7, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:55:49'),
(40, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:56:05'),
(41, 4, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-23 12:56:16'),
(42, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-24 10:51:24'),
(43, 16, 'Login', 'Collins logged in as Resident', 'User', 16, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-24 10:53:13'),
(44, 16, 'Create', 'Invitation created for Isaac Kagwe (token: 92DFA7FE8A)', 'VisitorInvitation', 5, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-24 10:53:54'),
(45, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-24 10:54:12'),
(46, 14, 'CheckIn', 'Pre-registered: Isaac Kagwe checked into C508 via invitation', 'Visit', 8, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-24 10:54:59'),
(47, 14, 'CheckOut', 'Muthama Michelle checked out from B505. Duration: 42885 min', 'Visit', 3, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-24 10:55:19'),
(48, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-24 10:55:43'),
(49, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:04:50'),
(50, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:08:00'),
(51, 17, 'Login', 'Ogega logged in as Resident', 'User', 17, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:08:47'),
(52, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:09:29'),
(53, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:12:15'),
(54, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:12:48'),
(55, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:14:45'),
(56, 10, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:16:34'),
(57, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:16:41'),
(58, 4, 'CheckOut', 'Isaac Kagwe checked out from C508. Duration: 7341 min', 'Visit', 8, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-04-29 13:16:50'),
(59, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 10:52:38'),
(60, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:15:49'),
(61, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:28:56'),
(62, 4, 'CheckOut', 'Nameless Kagwe checked out of B203. Duration: 11433 min', 'Visit', 7, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:29:20'),
(63, 4, 'CheckOut', 'Dianne Abby checked out of A101. Duration: 52995 min', 'Visit', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:29:23'),
(64, 4, 'CheckIn', 'Walk-in: Wendy Wariara → A606 (Personal Visit). Guard: Paul', 'Visit', 9, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:30:24'),
(65, 15, 'Login', 'Wahu logged in as Resident', 'User', 15, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:30:54'),
(66, 17, 'Login', 'Ogega logged in as Resident', 'User', 17, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:31:03'),
(67, 13, 'Login', 'Alice logged in as Resident', 'User', 13, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:31:10'),
(68, 12, 'Login', 'Brian logged in as Resident', 'User', 12, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:31:23'),
(69, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:31:58'),
(70, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:32:23'),
(71, 11, 'Create', 'Invitation created for Alice Kioko (token: 6974032A65)', 'VisitorInvitation', 6, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:33:07'),
(72, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:33:23'),
(73, 14, 'CheckIn', 'Pre-registered: Alice Kioko → A606 via invitation #6', 'Visit', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:34:58'),
(74, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:35:51'),
(75, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:38:40'),
(76, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:40:26'),
(77, 11, 'Logout', 'User signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:40:50'),
(78, 17, 'Login', 'Ogega logged in as Resident', 'User', 17, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:40:56'),
(79, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 11:42:01'),
(80, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 12:14:00'),
(81, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 12:30:19'),
(82, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 13:56:35'),
(83, 18, 'Login', 'Lucy  logged in as Resident', 'User', 18, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 14:00:18'),
(84, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-01 14:01:10'),
(85, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-02 01:17:56'),
(86, 4, 'CheckIn', 'Walk-in: Ryan Wakeba → A606 (Personal Visit). Guard: Paul', 'Visit', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-02 01:19:35'),
(87, 13, 'Login', 'Alice logged in as Resident', 'User', 13, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-02 01:20:05'),
(88, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-02 01:20:16'),
(89, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-02 01:21:13'),
(90, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-02 01:22:13'),
(91, 19, 'Login', 'Neila logged in as Resident', 'User', 19, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:30:56'),
(92, 19, 'Logout', 'neilaw@mgenitrack.com signed out', 'User', NULL, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:31:05'),
(93, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:31:10'),
(94, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:31:50'),
(95, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:35:26'),
(96, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:35:40'),
(97, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:35:58'),
(98, 20, 'CheckOut', 'Alice Kioko checked out of A606. Duration: 3061 min', 'Visit', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:36:13'),
(99, 20, 'CheckOut', 'Ryan Wakeba checked out of A606. Duration: 2236 min', 'Visit', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:36:16'),
(100, 20, 'CheckOut', 'Wendy Wariara checked out of A606. Duration: 3065 min', 'Visit', 9, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:36:19'),
(101, 20, 'CheckIn', 'Walk-in: Rexy  Muthama → B505 (Maintenance / Repair). Guard: Lucy ', 'Visit', 12, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:37:36'),
(102, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:38:57'),
(103, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-03 14:43:22'),
(104, 15, 'Login', 'Wahu logged in as Resident', 'User', 15, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 14:41:21'),
(105, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 14:41:36'),
(106, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:01:50'),
(107, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:07:11'),
(108, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:08:07'),
(109, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:14:45'),
(110, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:24:22'),
(111, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:24:41'),
(112, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:25:27'),
(113, 14, 'CheckIn', 'Walk-in: Shiko Stylist → B505 (Family). Guard: Eric', 'Visit', 13, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:26:35'),
(114, 13, 'Login', 'Alice logged in as Resident', 'User', 13, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:26:45'),
(115, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:26:57'),
(116, 12, 'Login', 'Brian logged in as Resident', 'User', 12, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:27:59'),
(117, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:40:37'),
(118, 21, 'Login', 'Rebecca logged in as Resident', 'User', 21, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 16:42:34'),
(119, 21, 'Login', 'Rebecca logged in as Resident', 'User', 21, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:08:50'),
(120, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:09:20'),
(121, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:27:32'),
(122, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:30:50'),
(123, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:31:29'),
(124, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:47:14'),
(125, 18, 'Login', 'Lucy  logged in as Resident', 'User', 18, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:48:12'),
(126, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:48:20'),
(127, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:48:55'),
(128, 4, 'Login', 'Paul logged in as Guard', 'User', 4, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:49:24'),
(129, 17, 'Login', 'Ogega logged in as Resident', 'User', 17, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:49:40'),
(130, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:49:52'),
(131, 22, 'Login', 'William logged in as Resident', 'User', 22, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:52:13'),
(132, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:52:26'),
(133, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:56:19'),
(134, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:59:20'),
(135, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 17:59:39'),
(136, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:01:07'),
(137, 20, 'CheckOut', 'Rexy  Muthama checked out of B505. Duration: 1643 min', 'Visit', 12, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:01:15'),
(138, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:01:32'),
(139, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:01:52'),
(140, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:13:08'),
(141, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:13:51'),
(142, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:14:16'),
(143, 18, 'Login', 'Lucy  logged in as Resident', 'User', 18, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:14:49'),
(144, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:14:58'),
(145, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:15:25'),
(146, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:36:05'),
(147, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:47:42'),
(148, 23, 'Login', 'Ruler logged in as Resident', 'User', 23, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:51:38'),
(149, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:51:53'),
(150, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:53:34'),
(151, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 18:54:19'),
(152, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-04 19:03:44'),
(153, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 12:25:03'),
(154, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 12:50:03'),
(155, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 12:54:59'),
(156, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 13:20:53'),
(157, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 13:30:13'),
(158, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 13:35:50'),
(159, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 14:36:01'),
(160, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 14:42:42'),
(161, 25, 'Login', 'Suo logged in as Resident', 'User', 25, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 14:53:47'),
(162, 24, 'Login', 'Faouzia  logged in as Resident', 'User', 24, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 14:54:00'),
(163, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 14:54:12'),
(164, 25, 'Login', 'Suo logged in as Resident', 'User', 25, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 14:58:21'),
(165, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 14:58:31'),
(166, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:13:15'),
(167, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:20:17'),
(168, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:21:01'),
(169, 14, 'CheckOut', 'Shiko Stylist checked out of B505. Duration: 2814 min', 'Visit', 13, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:21:09'),
(170, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:21:45'),
(171, 20, 'CheckIn', 'Walk-in: Susan Odum → C104 (BnB Stay). Guard: Lucy ', 'Visit', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:23:13'),
(172, 22, 'Login', 'William logged in as Resident', 'User', 22, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:23:57'),
(173, 22, 'Create', 'Invitation created for Rebecca Rose (token: 56AF98A230)', 'VisitorInvitation', 7, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:25:08'),
(174, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:25:23'),
(175, 20, 'CheckIn', 'Pre-registered: Rebecca Rose → C104 via invitation #7', 'Visit', 15, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:27:03'),
(176, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:27:31'),
(177, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-06 15:27:51'),
(178, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 13:43:24'),
(179, 12, 'Login', 'Brian logged in as Resident', 'User', 12, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 13:47:27'),
(180, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 13:48:59'),
(181, 22, 'Login', 'William logged in as Resident', 'User', 22, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 13:49:55'),
(182, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 13:50:08'),
(183, 14, 'CheckOut', 'Rebecca Rose checked out of C104. Duration: 4223 min', 'Visit', 15, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 13:50:14'),
(184, 14, 'CheckOut', 'Susan Odum checked out of C104. Duration: 4227 min', 'Visit', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 13:50:17'),
(185, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 13:50:30'),
(186, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 14:26:37'),
(187, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 14:33:46'),
(188, 11, 'Login', 'Anthony logged in as Resident', 'User', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 14:37:52'),
(189, 11, 'Create', 'Invitation created for Paul Walkerr (token: 672AF94149)', 'VisitorInvitation', 8, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 14:38:31'),
(190, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 14:38:43'),
(191, 14, 'CheckIn', 'Pre-registered: Paul Walkerr → A606 via invitation #8', 'Visit', 16, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 14:39:39'),
(192, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36 Edg/147.0.0.0', '2026-05-09 14:40:10'),
(193, 10, 'Login', 'Nancy logged in as PropertyManager', 'User', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 13:09:37'),
(194, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 13:12:08'),
(195, 1, 'Deactivate', 'User Suo Otunga deactivated', 'User', 25, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 13:13:58'),
(196, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 13:15:35'),
(197, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 13:25:28'),
(198, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 13:39:00'),
(199, 14, 'CheckOut', 'Paul Walkerr checked out of A606. Duration: 15779 min', 'Visit', 16, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 13:39:05'),
(200, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 13:56:58'),
(201, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 14:01:40'),
(202, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 14:06:39'),
(203, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 14:07:57'),
(204, 26, 'Login', 'Daggy logged in as Resident', 'User', 26, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 14:45:58'),
(205, 26, 'Login', 'Daggy logged in as Resident', 'User', 26, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 14:46:10'),
(206, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0', '2026-05-20 14:46:19'),
(207, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 09:43:30'),
(208, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 09:48:08'),
(209, 26, 'Login', 'Daggy logged in as Resident', 'User', 26, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 09:48:45'),
(210, 26, 'Create', 'Invitation created for Sharon Nyambura (token: 6F2312933B)', 'VisitorInvitation', 9, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 09:50:00'),
(211, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 09:50:11'),
(212, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 10:08:20'),
(213, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 10:08:53'),
(214, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 10:20:50'),
(215, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 10:26:42'),
(216, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 10:30:50'),
(217, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 11:48:08'),
(218, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 15:53:24'),
(219, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-11 15:56:15'),
(220, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 11:40:29'),
(221, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 12:27:58'),
(222, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 12:31:02');
INSERT INTO `activity_logs` (`log_id`, `user_id`, `action_type`, `action_details`, `related_entity_type`, `related_entity_id`, `ip_address`, `user_agent`, `time_stamp`) VALUES
(223, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 12:58:56'),
(224, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 12:59:39'),
(225, 55, 'Login', 'Lilian logged in as Resident', 'User', 55, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 13:00:57'),
(226, 55, 'Create', 'Invitation created for Rex Maasai (token: D70CBB00C2)', 'VisitorInvitation', 10, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 13:01:43'),
(227, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 13:01:56'),
(228, 65, 'Login', 'Irene logged in as Resident', 'User', 65, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 13:03:29'),
(229, 65, 'Create', 'Invitation created for Noni Gathoni (token: 6A24FA87F4)', 'VisitorInvitation', 11, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 13:04:08'),
(230, 20, 'Login', 'Lucy  logged in as Guard', 'User', 20, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 13:04:20'),
(231, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 13:04:32'),
(232, 14, 'CheckIn', 'Pre-registered: Rex Maasai → B309 via invitation #10', 'Visit', 17, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-15 13:05:17'),
(233, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:42:58'),
(234, 21, 'Login', 'Rebecca logged in as Resident', 'User', 21, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:44:59'),
(235, 21, 'Create', 'Invitation created for Michael Kinyua (token: 5B215D9356)', 'VisitorInvitation', 12, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:48:08'),
(236, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:48:24'),
(237, 14, 'CheckIn', 'Pre-registered: Michael Kinyua → C201 via invitation #12', 'Visit', 18, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:49:00'),
(238, 21, 'Login', 'Rebecca logged in as Resident', 'User', 21, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:49:10'),
(239, 14, 'Login', 'Eric logged in as Guard', 'User', 14, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:51:07'),
(240, 14, 'CheckIn', 'Walk-in: Barasa Joel → B503 (Delivery). Guard: Eric', 'Visit', 19, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:52:00'),
(241, 1, 'Login', 'Miano logged in as SuperAdmin', 'User', 1, '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/149.0.0.0 Safari/537.36 Edg/149.0.0.0', '2026-06-17 10:55:21');

-- --------------------------------------------------------

--
-- Table structure for table `notifications`
--

CREATE TABLE `notifications` (
  `notification_id` int(11) NOT NULL,
  `resident_id` int(11) DEFAULT NULL,
  `visit_id` int(11) DEFAULT NULL,
  `message` text DEFAULT NULL,
  `is_read` tinyint(1) DEFAULT 0,
  `created_at` datetime DEFAULT current_timestamp(),
  `notification_type` varchar(50) DEFAULT 'General',
  `title` varchar(100) DEFAULT NULL,
  `is_email_sent` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `notifications`
--

INSERT INTO `notifications` (`notification_id`, `resident_id`, `visit_id`, `message`, `is_read`, `created_at`, `notification_type`, `title`, `is_email_sent`) VALUES
(5, 4, 4, 'Dianne Abby has left your unit A101. Duration: 52995 min.', 1, '2026-05-01 11:29:23', 'CheckedOut', 'Visitor Checked Out', 0),
(6, 2, 9, 'Wendy Wariara has arrived at your unit A606.', 1, '2026-05-01 11:30:24', 'VisitorArrived', 'Visitor Arrived', 0),
(7, 2, 10, 'Alice Kioko has arrived at your unit A606.', 1, '2026-05-01 11:34:58', 'VisitorArrived', 'Visitor Arrived', 0),
(8, 2, 11, 'Ryan Wakeba has arrived at your unit A606.', 1, '2026-05-02 01:19:34', 'VisitorArrived', 'Visitor Arrived', 0),
(9, 2, 10, 'Alice Kioko has left your unit A606. Duration: 3061 min.', 1, '2026-05-03 14:36:13', 'CheckedOut', 'Visitor Checked Out', 0),
(10, 2, 11, 'Ryan Wakeba has left your unit A606. Duration: 2236 min.', 1, '2026-05-03 14:36:16', 'CheckedOut', 'Visitor Checked Out', 0),
(11, 2, 9, 'Wendy Wariara has left your unit A606. Duration: 3065 min.', 1, '2026-05-03 14:36:19', 'CheckedOut', 'Visitor Checked Out', 0),
(12, 3, 12, 'Rexy  Muthama has arrived at your unit B505.', 1, '2026-05-03 14:37:36', 'VisitorArrived', 'Visitor Arrived', 0),
(13, 3, 13, 'Shiko Stylist has arrived at your unit B505.', 1, '2026-05-04 16:26:35', 'VisitorArrived', 'Visitor Arrived', 0),
(14, 3, 12, 'Rexy  Muthama has left your unit B505. Duration: 1643 min.', 1, '2026-05-04 18:01:15', 'CheckedOut', 'Visitor Checked Out', 0),
(15, 3, 13, 'Shiko Stylist has left your unit B505. Duration: 2814 min.', 1, '2026-05-06 15:21:08', 'CheckedOut', 'Visitor Checked Out', 0),
(16, 11, 14, 'Susan Odum has arrived at your unit C104.', 1, '2026-05-06 15:23:13', 'VisitorArrived', 'Visitor Arrived', 0),
(17, 11, 15, 'Rebecca Rose has arrived at your unit C104.', 1, '2026-05-06 15:27:03', 'VisitorArrived', 'Visitor Arrived', 0),
(18, 11, 15, 'Rebecca Rose has left your unit C104. Duration: 4223 min.', 0, '2026-05-09 13:50:14', 'CheckedOut', 'Visitor Checked Out', 0),
(19, 11, 14, 'Susan Odum has left your unit C104. Duration: 4227 min.', 0, '2026-05-09 13:50:17', 'CheckedOut', 'Visitor Checked Out', 0),
(20, 2, 16, 'Paul Walkerr has arrived at your unit A606.', 0, '2026-05-09 14:39:39', 'VisitorArrived', 'Visitor Arrived', 0),
(21, 2, 16, 'Paul Walkerr has left your unit A606. Duration: 15779 min.', 0, '2026-05-20 13:39:05', 'CheckedOut', 'Visitor Checked Out', 0),
(22, 28, 17, 'Rex Maasai has arrived at your unit B309.', 0, '2026-06-15 13:05:17', 'VisitorArrived', 'Visitor Arrived', 0),
(23, 10, 18, 'Michael Kinyua has arrived at your unit C201.', 1, '2026-06-17 10:49:00', 'VisitorArrived', 'Visitor Arrived', 0);

-- --------------------------------------------------------

--
-- Table structure for table `reports`
--

CREATE TABLE `reports` (
  `report_id` int(11) NOT NULL,
  `generated_by` int(11) DEFAULT NULL,
  `report_type` varchar(100) DEFAULT NULL,
  `date_from` date DEFAULT NULL,
  `date_to` date DEFAULT NULL,
  `total_visitors` int(11) DEFAULT NULL,
  `total_check_ins` int(11) DEFAULT NULL,
  `total_check_outs` int(11) DEFAULT NULL,
  `file_path` varchar(255) DEFAULT NULL,
  `file_format` varchar(50) DEFAULT NULL,
  `generated_at` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `reports`
--

INSERT INTO `reports` (`report_id`, `generated_by`, `report_type`, `date_from`, `date_to`, `total_visitors`, `total_check_ins`, `total_check_outs`, `file_path`, `file_format`, `generated_at`) VALUES
(1, 1, 'Visitor Summary', '2026-02-23', '2026-03-25', 2, 2, 1, '/reports/Visitor Summary_20260325_160458.csv', 'CSV', '2026-03-25 16:04:58'),
(2, 1, 'Visitor Summary', '2026-02-23', '2026-03-25', 2, 2, 1, '/reports/Visitor Summary_20260325_160503.csv', 'CSV', '2026-03-25 16:05:03'),
(3, 1, 'Daily Activity', '2026-02-24', '2026-03-26', 4, 4, 1, '/reports/Daily Activity_20260326_132315.html', 'HTML', '2026-03-26 13:23:15'),
(4, 1, 'Check-In Log', '2026-02-24', '2026-03-26', 4, 4, 1, '/reports/Check-In Log_20260326_132340.html', 'HTML', '2026-03-26 13:23:40'),
(5, 1, 'Daily Activity', '2026-03-23', '2026-04-22', 4, 4, 1, '/reports/Daily Activity_20260422_132757.html', 'HTML', '2026-04-22 13:27:57'),
(6, 1, 'Security Audit', '2026-03-23', '2026-04-22', 4, 4, 1, '/reports/Security Audit_20260422_132902.html', 'HTML', '2026-04-22 13:29:02'),
(7, 1, 'Check-In Log', '2026-03-24', '2026-04-23', 5, 5, 3, '/reports/Check-In Log_20260423_125036.html', 'HTML', '2026-04-23 12:50:36'),
(8, 1, 'Visitor Summary', '2026-03-25', '2026-04-24', 7, 7, 4, '/reports/Visitor Summary_20260424_105552.html', 'HTML', '2026-04-24 10:55:52'),
(9, 1, 'Visitor Summary', '2026-03-25', '2026-04-24', 7, 7, 4, '/reports/Visitor Summary_20260424_105631.html', 'HTML', '2026-04-24 10:56:31'),
(10, 1, 'Visitor Summary', '2026-04-04', '2026-05-04', 8, 8, 7, '/reports/Visitor Summary_20260504_181614.html', 'HTML', '2026-05-04 18:16:14'),
(11, 10, 'Security Audit', '2026-04-09', '2026-05-09', 11, 11, 10, '/reports/Security Audit_20260509_144112.html', 'HTML', '2026-05-09 14:41:12'),
(12, 1, 'Security Audit', '2026-05-12', '2026-06-11', 0, 0, 0, '/reports/Security Audit_20260611_094414.html', 'HTML', '2026-06-11 09:44:14'),
(13, 1, 'Security Audit', '2026-05-01', '2026-06-11', 8, 8, 8, '/reports/Security Audit_20260611_094441.html', 'HTML', '2026-06-11 09:44:41'),
(14, 1, 'Visitor Summary', '2026-04-23', '2026-06-17', 13, 13, 10, '/reports/Visitor Summary_20260617_105553.html', 'HTML', '2026-06-17 10:55:53');

-- --------------------------------------------------------

--
-- Table structure for table `residents`
--

CREATE TABLE `residents` (
  `resident_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `house_number` varchar(20) NOT NULL,
  `unit_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `residents`
--

INSERT INTO `residents` (`resident_id`, `user_id`, `house_number`, `unit_id`) VALUES
(2, 11, 'A606', 56),
(3, 12, 'B505', 105),
(4, 13, 'A101', 1),
(5, 15, 'B203', 78),
(6, 16, 'C508', 168),
(7, 17, 'C407', 157),
(8, 18, 'C302', 142),
(9, 19, 'A303', 23),
(10, 21, 'C201', 131),
(11, 22, 'C104', 124),
(14, 24, 'A503', 43),
(15, 25, 'A501', 161),
(16, 26, 'B203', 73),
(17, 28, 'A102', 2),
(18, 29, 'B101', 61),
(19, 30, 'C603', 173),
(20, 31, 'A203', 13),
(21, 85, 'A205', 15),
(23, 32, 'C606', 176),
(24, 33, 'A202', 12),
(25, 82, 'A204', 14),
(26, 34, 'B109', 69),
(27, 66, 'C608', 178),
(28, 55, 'B309', 89),
(29, 35, 'B601', 111),
(30, 79, 'B301', 81),
(31, 36, 'B501', 101),
(32, 37, 'A504', 44),
(33, 61, 'A103', 3),
(34, 38, 'A104', 4),
(35, 40, 'A105', 5),
(36, 39, 'B602', 112),
(37, 41, 'B401', 91),
(38, 83, 'A301', 21),
(39, 86, 'B404', 94),
(40, 87, 'C208', 138),
(41, 81, 'C105', 125),
(42, 42, 'A106', 6),
(43, 43, 'B603', 113),
(44, 50, 'B106', 66),
(45, 44, 'C202', 132),
(46, 59, 'A603', 53),
(47, 76, 'C203', 133),
(48, 74, 'C406', 156),
(49, 72, 'B406', 96),
(50, 65, 'C408', 158);

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `role_id` int(11) NOT NULL,
  `role_name` varchar(50) NOT NULL,
  `role_description` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`role_id`, `role_name`, `role_description`) VALUES
(1, 'SuperAdmin', 'System Super Administrator'),
(2, 'PropertyManager', 'Manages property operations'),
(3, 'Guard', 'Security guard personnel'),
(4, 'Resident', 'Apartment resident or house owne');

-- --------------------------------------------------------

--
-- Table structure for table `system_settings`
--

CREATE TABLE `system_settings` (
  `setting_id` int(11) NOT NULL,
  `setting_key` varchar(100) DEFAULT NULL,
  `setting_value` varchar(255) DEFAULT NULL,
  `sys_description` text DEFAULT NULL,
  `updated_at` datetime DEFAULT current_timestamp(),
  `updated_by` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `units`
--

CREATE TABLE `units` (
  `unit_id` int(11) NOT NULL,
  `unit_number` varchar(10) NOT NULL,
  `block` char(1) NOT NULL,
  `floor_number` int(11) NOT NULL,
  `unit_position` int(11) NOT NULL,
  `unit_type` enum('Residential','BnB') NOT NULL,
  `is_occupied` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `units`
--

INSERT INTO `units` (`unit_id`, `unit_number`, `block`, `floor_number`, `unit_position`, `unit_type`, `is_occupied`, `created_at`) VALUES
(1, 'A101', 'A', 1, 1, 'Residential', 1, '2026-04-22 14:25:07'),
(2, 'A102', 'A', 1, 2, 'Residential', 1, '2026-04-22 14:25:07'),
(3, 'A103', 'A', 1, 3, 'Residential', 1, '2026-04-22 14:25:07'),
(4, 'A104', 'A', 1, 4, 'Residential', 1, '2026-04-22 14:25:07'),
(5, 'A105', 'A', 1, 5, 'Residential', 1, '2026-04-22 14:25:07'),
(6, 'A106', 'A', 1, 6, 'Residential', 1, '2026-04-22 14:25:07'),
(7, 'A107', 'A', 1, 7, 'Residential', 0, '2026-04-22 14:25:07'),
(8, 'A108', 'A', 1, 8, 'Residential', 0, '2026-04-22 14:25:07'),
(9, 'A109', 'A', 1, 9, 'Residential', 0, '2026-04-22 14:25:07'),
(10, 'A110', 'A', 1, 10, 'Residential', 0, '2026-04-22 14:25:07'),
(11, 'A201', 'A', 2, 1, 'Residential', 0, '2026-04-22 14:25:07'),
(12, 'A202', 'A', 2, 2, 'Residential', 1, '2026-04-22 14:25:07'),
(13, 'A203', 'A', 2, 3, 'Residential', 1, '2026-04-22 14:25:07'),
(14, 'A204', 'A', 2, 4, 'Residential', 1, '2026-04-22 14:25:07'),
(15, 'A205', 'A', 2, 5, 'Residential', 1, '2026-04-22 14:25:07'),
(16, 'A206', 'A', 2, 6, 'Residential', 0, '2026-04-22 14:25:07'),
(17, 'A207', 'A', 2, 7, 'Residential', 0, '2026-04-22 14:25:07'),
(18, 'A208', 'A', 2, 8, 'Residential', 0, '2026-04-22 14:25:07'),
(19, 'A209', 'A', 2, 9, 'Residential', 0, '2026-04-22 14:25:07'),
(20, 'A210', 'A', 2, 10, 'Residential', 0, '2026-04-22 14:25:07'),
(21, 'A301', 'A', 3, 1, 'Residential', 1, '2026-04-22 14:25:07'),
(22, 'A302', 'A', 3, 2, 'Residential', 0, '2026-04-22 14:25:07'),
(23, 'A303', 'A', 3, 3, 'Residential', 1, '2026-04-22 14:25:07'),
(24, 'A304', 'A', 3, 4, 'Residential', 0, '2026-04-22 14:25:07'),
(25, 'A305', 'A', 3, 5, 'Residential', 0, '2026-04-22 14:25:07'),
(26, 'A306', 'A', 3, 6, 'Residential', 0, '2026-04-22 14:25:07'),
(27, 'A307', 'A', 3, 7, 'Residential', 0, '2026-04-22 14:25:07'),
(28, 'A308', 'A', 3, 8, 'Residential', 0, '2026-04-22 14:25:07'),
(29, 'A309', 'A', 3, 9, 'Residential', 0, '2026-04-22 14:25:07'),
(30, 'A310', 'A', 3, 10, 'Residential', 0, '2026-04-22 14:25:07'),
(31, 'A401', 'A', 4, 1, 'Residential', 0, '2026-04-22 14:25:07'),
(32, 'A402', 'A', 4, 2, 'Residential', 0, '2026-04-22 14:25:07'),
(33, 'A403', 'A', 4, 3, 'Residential', 0, '2026-04-22 14:25:07'),
(34, 'A404', 'A', 4, 4, 'Residential', 0, '2026-04-22 14:25:07'),
(35, 'A405', 'A', 4, 5, 'Residential', 0, '2026-04-22 14:25:07'),
(36, 'A406', 'A', 4, 6, 'Residential', 0, '2026-04-22 14:25:07'),
(37, 'A407', 'A', 4, 7, 'Residential', 0, '2026-04-22 14:25:07'),
(38, 'A408', 'A', 4, 8, 'Residential', 0, '2026-04-22 14:25:07'),
(39, 'A409', 'A', 4, 9, 'Residential', 0, '2026-04-22 14:25:07'),
(40, 'A410', 'A', 4, 10, 'Residential', 0, '2026-04-22 14:25:07'),
(41, 'A501', 'A', 5, 1, 'Residential', 0, '2026-04-22 14:25:07'),
(42, 'A502', 'A', 5, 2, 'Residential', 0, '2026-04-22 14:25:07'),
(43, 'A503', 'A', 5, 3, 'Residential', 1, '2026-04-22 14:25:07'),
(44, 'A504', 'A', 5, 4, 'Residential', 1, '2026-04-22 14:25:07'),
(45, 'A505', 'A', 5, 5, 'Residential', 0, '2026-04-22 14:25:07'),
(46, 'A506', 'A', 5, 6, 'Residential', 0, '2026-04-22 14:25:07'),
(47, 'A507', 'A', 5, 7, 'Residential', 0, '2026-04-22 14:25:07'),
(48, 'A508', 'A', 5, 8, 'Residential', 0, '2026-04-22 14:25:07'),
(49, 'A509', 'A', 5, 9, 'Residential', 0, '2026-04-22 14:25:07'),
(50, 'A510', 'A', 5, 10, 'Residential', 0, '2026-04-22 14:25:07'),
(51, 'A601', 'A', 6, 1, 'Residential', 0, '2026-04-22 14:25:07'),
(52, 'A602', 'A', 6, 2, 'Residential', 0, '2026-04-22 14:25:07'),
(53, 'A603', 'A', 6, 3, 'Residential', 1, '2026-04-22 14:25:07'),
(54, 'A604', 'A', 6, 4, 'Residential', 0, '2026-04-22 14:25:07'),
(55, 'A605', 'A', 6, 5, 'Residential', 0, '2026-04-22 14:25:07'),
(56, 'A606', 'A', 6, 6, 'Residential', 1, '2026-04-22 14:25:07'),
(57, 'A607', 'A', 6, 7, 'Residential', 0, '2026-04-22 14:25:07'),
(58, 'A608', 'A', 6, 8, 'Residential', 0, '2026-04-22 14:25:07'),
(59, 'A609', 'A', 6, 9, 'Residential', 0, '2026-04-22 14:25:07'),
(60, 'A610', 'A', 6, 10, 'Residential', 0, '2026-04-22 14:25:07'),
(61, 'B101', 'B', 1, 1, 'Residential', 1, '2026-04-22 14:25:42'),
(62, 'B102', 'B', 1, 2, 'Residential', 0, '2026-04-22 14:25:42'),
(63, 'B103', 'B', 1, 3, 'Residential', 0, '2026-04-22 14:25:42'),
(64, 'B104', 'B', 1, 4, 'Residential', 0, '2026-04-22 14:25:42'),
(65, 'B105', 'B', 1, 5, 'Residential', 0, '2026-04-22 14:25:42'),
(66, 'B106', 'B', 1, 6, 'Residential', 1, '2026-04-22 14:25:42'),
(67, 'B107', 'B', 1, 7, 'Residential', 0, '2026-04-22 14:25:42'),
(68, 'B108', 'B', 1, 8, 'Residential', 0, '2026-04-22 14:25:42'),
(69, 'B109', 'B', 1, 9, 'Residential', 1, '2026-04-22 14:25:42'),
(70, 'B110', 'B', 1, 10, 'Residential', 0, '2026-04-22 14:25:42'),
(71, 'B201', 'B', 2, 1, 'Residential', 0, '2026-04-22 14:25:42'),
(72, 'B202', 'B', 2, 2, 'Residential', 0, '2026-04-22 14:25:42'),
(73, 'B203', 'B', 2, 3, 'Residential', 1, '2026-04-22 14:25:42'),
(74, 'B204', 'B', 2, 4, 'Residential', 0, '2026-04-22 14:25:42'),
(75, 'B205', 'B', 2, 5, 'Residential', 0, '2026-04-22 14:25:42'),
(76, 'B206', 'B', 2, 6, 'Residential', 0, '2026-04-22 14:25:42'),
(77, 'B207', 'B', 2, 7, 'Residential', 0, '2026-04-22 14:25:42'),
(78, 'B208', 'B', 2, 8, 'Residential', 1, '2026-04-22 14:25:42'),
(79, 'B209', 'B', 2, 9, 'Residential', 0, '2026-04-22 14:25:42'),
(80, 'B210', 'B', 2, 10, 'Residential', 0, '2026-04-22 14:25:42'),
(81, 'B301', 'B', 3, 1, 'Residential', 1, '2026-04-22 14:25:42'),
(82, 'B302', 'B', 3, 2, 'Residential', 0, '2026-04-22 14:25:42'),
(83, 'B303', 'B', 3, 3, 'Residential', 0, '2026-04-22 14:25:42'),
(84, 'B304', 'B', 3, 4, 'Residential', 0, '2026-04-22 14:25:42'),
(85, 'B305', 'B', 3, 5, 'Residential', 0, '2026-04-22 14:25:42'),
(86, 'B306', 'B', 3, 6, 'Residential', 0, '2026-04-22 14:25:42'),
(87, 'B307', 'B', 3, 7, 'Residential', 0, '2026-04-22 14:25:42'),
(88, 'B308', 'B', 3, 8, 'Residential', 0, '2026-04-22 14:25:42'),
(89, 'B309', 'B', 3, 9, 'Residential', 1, '2026-04-22 14:25:42'),
(90, 'B310', 'B', 3, 10, 'Residential', 0, '2026-04-22 14:25:42'),
(91, 'B401', 'B', 4, 1, 'Residential', 1, '2026-04-22 14:25:42'),
(92, 'B402', 'B', 4, 2, 'Residential', 0, '2026-04-22 14:25:42'),
(93, 'B403', 'B', 4, 3, 'Residential', 0, '2026-04-22 14:25:42'),
(94, 'B404', 'B', 4, 4, 'Residential', 1, '2026-04-22 14:25:42'),
(95, 'B405', 'B', 4, 5, 'Residential', 0, '2026-04-22 14:25:42'),
(96, 'B406', 'B', 4, 6, 'Residential', 1, '2026-04-22 14:25:42'),
(97, 'B407', 'B', 4, 7, 'Residential', 0, '2026-04-22 14:25:42'),
(98, 'B408', 'B', 4, 8, 'Residential', 0, '2026-04-22 14:25:42'),
(99, 'B409', 'B', 4, 9, 'Residential', 0, '2026-04-22 14:25:42'),
(100, 'B410', 'B', 4, 10, 'Residential', 0, '2026-04-22 14:25:42'),
(101, 'B501', 'B', 5, 1, 'Residential', 1, '2026-04-22 14:25:42'),
(102, 'B502', 'B', 5, 2, 'Residential', 0, '2026-04-22 14:25:42'),
(103, 'B503', 'B', 5, 3, 'Residential', 1, '2026-04-22 14:25:42'),
(104, 'B504', 'B', 5, 4, 'Residential', 0, '2026-04-22 14:25:42'),
(105, 'B505', 'B', 5, 5, 'Residential', 1, '2026-04-22 14:25:42'),
(106, 'B506', 'B', 5, 6, 'Residential', 0, '2026-04-22 14:25:42'),
(107, 'B507', 'B', 5, 7, 'Residential', 0, '2026-04-22 14:25:42'),
(108, 'B508', 'B', 5, 8, 'Residential', 0, '2026-04-22 14:25:42'),
(109, 'B509', 'B', 5, 9, 'Residential', 0, '2026-04-22 14:25:42'),
(110, 'B510', 'B', 5, 10, 'Residential', 0, '2026-04-22 14:25:42'),
(111, 'B601', 'B', 6, 1, 'Residential', 1, '2026-04-22 14:25:42'),
(112, 'B602', 'B', 6, 2, 'Residential', 1, '2026-04-22 14:25:42'),
(113, 'B603', 'B', 6, 3, 'Residential', 1, '2026-04-22 14:25:42'),
(114, 'B604', 'B', 6, 4, 'Residential', 0, '2026-04-22 14:25:42'),
(115, 'B605', 'B', 6, 5, 'Residential', 0, '2026-04-22 14:25:42'),
(116, 'B606', 'B', 6, 6, 'Residential', 0, '2026-04-22 14:25:42'),
(117, 'B607', 'B', 6, 7, 'Residential', 0, '2026-04-22 14:25:42'),
(118, 'B608', 'B', 6, 8, 'Residential', 0, '2026-04-22 14:25:42'),
(119, 'B609', 'B', 6, 9, 'Residential', 0, '2026-04-22 14:25:42'),
(120, 'B610', 'B', 6, 10, 'Residential', 0, '2026-04-22 14:25:42'),
(121, 'C101', 'C', 1, 1, 'BnB', 0, '2026-04-22 14:26:32'),
(122, 'C102', 'C', 1, 2, 'BnB', 0, '2026-04-22 14:26:32'),
(123, 'C103', 'C', 1, 3, 'BnB', 0, '2026-04-22 14:26:32'),
(124, 'C104', 'C', 1, 4, 'BnB', 1, '2026-04-22 14:26:32'),
(125, 'C105', 'C', 1, 5, 'BnB', 1, '2026-04-22 14:26:32'),
(126, 'C106', 'C', 1, 6, 'BnB', 0, '2026-04-22 14:26:32'),
(127, 'C107', 'C', 1, 7, 'BnB', 0, '2026-04-22 14:26:32'),
(128, 'C108', 'C', 1, 8, 'BnB', 0, '2026-04-22 14:26:32'),
(129, 'C109', 'C', 1, 9, 'BnB', 0, '2026-04-22 14:26:32'),
(130, 'C110', 'C', 1, 10, 'BnB', 0, '2026-04-22 14:26:32'),
(131, 'C201', 'C', 2, 1, 'BnB', 1, '2026-04-22 14:26:32'),
(132, 'C202', 'C', 2, 2, 'BnB', 1, '2026-04-22 14:26:32'),
(133, 'C203', 'C', 2, 3, 'BnB', 1, '2026-04-22 14:26:32'),
(134, 'C204', 'C', 2, 4, 'BnB', 0, '2026-04-22 14:26:32'),
(135, 'C205', 'C', 2, 5, 'BnB', 0, '2026-04-22 14:26:32'),
(136, 'C206', 'C', 2, 6, 'BnB', 0, '2026-04-22 14:26:32'),
(137, 'C207', 'C', 2, 7, 'BnB', 0, '2026-04-22 14:26:32'),
(138, 'C208', 'C', 2, 8, 'BnB', 1, '2026-04-22 14:26:32'),
(139, 'C209', 'C', 2, 9, 'BnB', 0, '2026-04-22 14:26:32'),
(140, 'C210', 'C', 2, 10, 'BnB', 0, '2026-04-22 14:26:32'),
(141, 'C301', 'C', 3, 1, 'BnB', 0, '2026-04-22 14:26:32'),
(142, 'C302', 'C', 3, 2, 'BnB', 1, '2026-04-22 14:26:32'),
(143, 'C303', 'C', 3, 3, 'BnB', 0, '2026-04-22 14:26:32'),
(144, 'C304', 'C', 3, 4, 'BnB', 0, '2026-04-22 14:26:32'),
(145, 'C305', 'C', 3, 5, 'BnB', 0, '2026-04-22 14:26:32'),
(146, 'C306', 'C', 3, 6, 'BnB', 0, '2026-04-22 14:26:32'),
(147, 'C307', 'C', 3, 7, 'BnB', 0, '2026-04-22 14:26:32'),
(148, 'C308', 'C', 3, 8, 'BnB', 0, '2026-04-22 14:26:32'),
(149, 'C309', 'C', 3, 9, 'BnB', 0, '2026-04-22 14:26:32'),
(150, 'C310', 'C', 3, 10, 'BnB', 0, '2026-04-22 14:26:32'),
(151, 'C401', 'C', 4, 1, 'BnB', 0, '2026-04-22 14:26:32'),
(152, 'C402', 'C', 4, 2, 'BnB', 0, '2026-04-22 14:26:32'),
(153, 'C403', 'C', 4, 3, 'BnB', 0, '2026-04-22 14:26:32'),
(154, 'C404', 'C', 4, 4, 'BnB', 0, '2026-04-22 14:26:32'),
(155, 'C405', 'C', 4, 5, 'BnB', 0, '2026-04-22 14:26:32'),
(156, 'C406', 'C', 4, 6, 'BnB', 1, '2026-04-22 14:26:32'),
(157, 'C407', 'C', 4, 7, 'BnB', 1, '2026-04-22 14:26:32'),
(158, 'C408', 'C', 4, 8, 'BnB', 1, '2026-04-22 14:26:32'),
(159, 'C409', 'C', 4, 9, 'BnB', 0, '2026-04-22 14:26:32'),
(160, 'C410', 'C', 4, 10, 'BnB', 0, '2026-04-22 14:26:32'),
(161, 'C501', 'C', 5, 1, 'BnB', 1, '2026-04-22 14:26:32'),
(162, 'C502', 'C', 5, 2, 'BnB', 0, '2026-04-22 14:26:32'),
(163, 'C503', 'C', 5, 3, 'BnB', 0, '2026-04-22 14:26:32'),
(164, 'C504', 'C', 5, 4, 'BnB', 0, '2026-04-22 14:26:32'),
(165, 'C505', 'C', 5, 5, 'BnB', 0, '2026-04-22 14:26:32'),
(166, 'C506', 'C', 5, 6, 'BnB', 0, '2026-04-22 14:26:32'),
(167, 'C507', 'C', 5, 7, 'BnB', 0, '2026-04-22 14:26:32'),
(168, 'C508', 'C', 5, 8, 'BnB', 1, '2026-04-22 14:26:32'),
(169, 'C509', 'C', 5, 9, 'BnB', 0, '2026-04-22 14:26:32'),
(170, 'C510', 'C', 5, 10, 'BnB', 0, '2026-04-22 14:26:32'),
(171, 'C601', 'C', 6, 1, 'BnB', 0, '2026-04-22 14:26:32'),
(172, 'C602', 'C', 6, 2, 'BnB', 0, '2026-04-22 14:26:32'),
(173, 'C603', 'C', 6, 3, 'BnB', 1, '2026-04-22 14:26:32'),
(174, 'C604', 'C', 6, 4, 'BnB', 0, '2026-04-22 14:26:32'),
(175, 'C605', 'C', 6, 5, 'BnB', 0, '2026-04-22 14:26:32'),
(176, 'C606', 'C', 6, 6, 'BnB', 1, '2026-04-22 14:26:32'),
(177, 'C607', 'C', 6, 7, 'BnB', 0, '2026-04-22 14:26:32'),
(178, 'C608', 'C', 6, 8, 'BnB', 1, '2026-04-22 14:26:32'),
(179, 'C609', 'C', 6, 9, 'BnB', 0, '2026-04-22 14:26:32'),
(180, 'C610', 'C', 6, 10, 'BnB', 0, '2026-04-22 14:26:32');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `firstname` varchar(50) NOT NULL,
  `secondname` varchar(50) DEFAULT NULL,
  `gender` enum('Male','Female','Other') DEFAULT NULL,
  `passwordhash` varchar(255) NOT NULL,
  `id_number` varchar(50) DEFAULT NULL,
  `email` varchar(100) NOT NULL,
  `phone_number` varchar(20) DEFAULT NULL,
  `user_status` enum('Active','Inactive') DEFAULT 'Active',
  `created_at` datetime DEFAULT current_timestamp(),
  `last_login` datetime DEFAULT NULL,
  `created_by` int(11) DEFAULT NULL,
  `session_expires_at` datetime DEFAULT NULL,
  `last_activity_at` datetime DEFAULT NULL,
  `AcceptedPrivacyPolicy` bit(1) NOT NULL DEFAULT b'0',
  `PrivacyAcceptedAt` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `firstname`, `secondname`, `gender`, `passwordhash`, `id_number`, `email`, `phone_number`, `user_status`, `created_at`, `last_login`, `created_by`, `session_expires_at`, `last_activity_at`, `AcceptedPrivacyPolicy`, `PrivacyAcceptedAt`) VALUES
(1, 'Miano', 'Joy', 'Female', 'AQAAAAIAAYagAAAAEN6aFXn1jzuLjz4l/0NAvSyU9wh7dStCo6M7wiu2ayIpuxDQS1dh3mGnfS1ho/obeA==', '3252560', 'mianojoy@icloud.com', '0745289794', 'Active', '2026-02-13 13:35:11', '2026-06-17 10:55:21', NULL, NULL, NULL, b'0', NULL),
(4, 'Paul', 'Odoyo', 'Male', 'AQAAAAIAAYagAAAAEP/ax1NfcdaR1d8O44LsK2f58x8fw5+1UhTPrEz/OnGvLqv6C45fFxtzNgebiXEYYg==', '349088', 'paulodo@mgenitrack.com', '0782406992', 'Active', '2026-03-03 13:57:44', '2026-05-04 17:49:24', 1, NULL, NULL, b'0', NULL),
(10, 'Nancy', 'Musau', 'Female', 'AQAAAAIAAYagAAAAEJeDtMNsXyZQfOcQbLYhCEsxTsQVKmNl4IlhfVLDeQHPWqPx3isOVMuw3JVhux6IzQ==', '678903546', 'nancyms@mgenitrack.com', '0765340988', 'Active', '2026-03-22 14:36:43', '2026-05-20 13:09:37', 1, NULL, NULL, b'0', NULL),
(11, 'Anthony', 'Rick', 'Male', 'AQAAAAIAAYagAAAAENU3CLw+48dWCAs/p3/6hFBH3enhOoM23Tca/A4fO55RXTUMXI3flATdlxLv5m839g==', '868179097', 'rianthony@mgenitrack.com', '0743678997', 'Active', '2026-03-22 14:41:02', '2026-05-09 14:37:52', 1, NULL, NULL, b'0', NULL),
(12, 'Brian', 'Makori', 'Male', 'AQAAAAIAAYagAAAAEI1JyQsjseEssnBP9P0pEvSZzgSZ8UHaEtNByeh05iUSjboaXNaggqyKlDMI4+/0TA==', '3278907', 'makoribr@mgenitrack.com', '0747770922', 'Active', '2026-03-25 15:04:11', '2026-05-09 13:47:27', 1, NULL, NULL, b'0', NULL),
(13, 'Alice', 'Mwihaki', 'Female', 'AQAAAAIAAYagAAAAEFi/95qtMYRrGxWZAp4sF51wkc8OgGhEeBXPfZ78x4MyeAvMDFhfWD8sJRmIRIw4Ug==', '7890254', 'mwihakial@mgenitrack.com', '0778309164', 'Active', '2026-03-25 15:05:31', '2026-05-04 16:26:45', 1, NULL, NULL, b'0', NULL),
(14, 'Eric', 'Shindeni', 'Male', 'AQAAAAIAAYagAAAAEK5fSm01XJMjshzE/heBxo5BvzN9zAyvfxTBGyn4UwaP5PVFV/m9aHO/Di/aZlYX8Q==', '87629087', 'ericshi@mgenitrack.com', '0789046732', 'Active', '2026-03-26 13:26:35', '2026-06-17 10:51:07', 1, NULL, NULL, b'0', NULL),
(15, 'Wahu', 'Kagwe', 'Female', 'AQAAAAIAAYagAAAAEAXnJiBnH2mdzh5vmHQUFl+Zjz6igPtMSLBue4hOdbI6GhNv6ViaYbiFRmd34t0TkQ==', '897526789', 'wahukags@mgenitrack.com', '0789245678', 'Active', '2026-04-23 12:48:32', '2026-05-04 14:41:21', 1, NULL, NULL, b'0', NULL),
(16, 'Collins', 'Kipkirui', 'Male', 'AQAAAAIAAYagAAAAEKmFxvSuboNvFypcXaVTKVebSZb7Dq/g1D97QF1T++E0hMpiVos6aaHmTxBAlZUptg==', '2456890', 'kipkirui@mgenitrack.com', '0789536789', 'Active', '2026-04-24 10:53:03', '2026-04-24 10:53:13', 1, NULL, NULL, b'0', NULL),
(17, 'Ogega', 'Desmay', 'Male', 'AQAAAAIAAYagAAAAEAsvWrupXgmvBstqun0RKeAKe0AfPgajPk12RbiNtO4WAYxOwmgFXC6lBxYh2BbMWw==', '5678902', 'desgeg@mgenitrack.com', '0789267189', 'Active', '2026-04-29 13:07:39', '2026-05-04 17:49:40', 1, NULL, NULL, b'0', NULL),
(18, 'Lucy ', 'Gitonga', 'Female', 'AQAAAAIAAYagAAAAEBsxK9F0Sd/jLh/9xmY0kZMOvU7VHueY43qJjQjvaEih1ifCwf/tqkqOeiHlgaFHfw==', '5678922', 'lucygitosh@mgenitrack.com', '0715567163', 'Active', '2026-05-01 13:58:05', '2026-05-04 18:14:49', 1, NULL, NULL, b'0', NULL),
(19, 'Neila', 'Wawira', 'Female', 'AQAAAAIAAYagAAAAEErV2QOwD9gg+3Scad3W7Qp1aZ/X136dmbLylvNVVafNMGKd0F44vHn0DaMtSTrO0g==', '00781917', 'neilaw@mgenitrack.com', '0782671563', 'Active', '2026-05-02 01:24:33', '2026-05-03 14:30:55', 1, NULL, NULL, b'0', NULL),
(20, 'Lucy ', 'Nekesa', 'Female', 'AQAAAAIAAYagAAAAEOXdH/GUWy9LF0TPuuhnRnRv9n3mpl7HKoyPO8iHvZs+uZCcOmuSFruPSvYhSe0uiA==', '43782911', 'nekesalu34@mgenitrack.com', '0725367291', 'Active', '2026-05-03 14:35:18', '2026-06-15 13:04:20', 1, NULL, NULL, b'0', NULL),
(21, 'Rebecca', 'Wanja', 'Female', 'AQAAAAIAAYagAAAAEDuNR7wxo4mXfD7lHEYlzhyMDurNc0wdpTtBi2//YINpjrAH8ARLqZq/mX1U/XvjMA==', '3553570', 'rebeccawa@mgenitrack.com', '0715567163', 'Active', '2026-05-04 16:42:23', '2026-06-17 10:49:10', 1, NULL, NULL, b'0', NULL),
(22, 'William', ' Kinuthia', 'Male', 'AQAAAAIAAYagAAAAEOAZgwK6LP8AKBfZavTflOAYTEt8/grnGLNdj43jPej1vyalo5RrsUdG8Fj069BYmQ==', '8739290033', 'kinut22@mgenitrack.com', '0765389267', 'Active', '2026-05-04 17:51:54', '2026-05-09 13:49:55', 1, NULL, NULL, b'0', NULL),
(23, 'Ruler', 'Musa', 'Male', 'AQAAAAIAAYagAAAAENDgRW27DyMUdKoLm0IRA2n/oW5wo1YT1t7FiexNGCPmN4vOJ0q2Qft2PKpQb/mbYg==', '78990223', 'musaru23@mgenitrack.com', '0789299201', 'Active', '2026-05-04 18:50:13', '2026-05-04 18:51:38', 1, NULL, NULL, b'0', NULL),
(24, 'Faouzia ', 'Rehema', 'Female', 'AQAAAAIAAYagAAAAEAz4BXvoWtXM0WVFSBpFV63LNL52KPUJb60TFt0/Q3P3MHoN4bWAiefWKbg8jEI1jg==', '44478933', 'rehemafaz@mgenitrack.com', '0743789202', 'Active', '2026-05-06 13:23:56', '2026-05-06 14:54:00', 1, NULL, NULL, b'0', NULL),
(25, 'Suo', 'Otunga', 'Male', 'AQAAAAIAAYagAAAAEMD+05W/nkaziE4Jd6p1LpqQDO7KB788dvz0DdXLz8gpT5SJOa4U+64kxLpoSDMX1g==', '540294855', 'suotunga@mgenitrack.com', '0732634556', 'Inactive', '2026-05-06 14:46:42', '2026-05-06 14:58:21', 1, NULL, NULL, b'0', NULL),
(26, 'Daggy', 'Wise', 'Male', 'AQAAAAIAAYagAAAAEFZsvVYA678sSONgF/SZ38tYB+YkxXOmJPU90q0A8G3pdNx89kX/X80iFLXNsNlZ7g==', '38221199', 'wisestdag@mgenitrack.com', '0713693079', 'Active', '2026-05-20 14:09:47', '2026-06-11 09:48:45', 1, NULL, NULL, b'1', NULL),
(27, 'Maureen ', 'Omondi', 'Female', 'AQAAAAIAAYagAAAAEA5tcv5G6PV0pj5xfS5WvvgSvJTmGcjfBjjBGDmoeqRmfK+FPA6A2MT3FrLzRvAh1g==', '56908765', 'omondimau@mgenitrack.com', '0766425383', 'Active', '2026-06-15 11:43:13', NULL, 1, NULL, NULL, b'0', NULL),
(28, 'Carl ', 'Joseph', 'Male', 'AQAAAAIAAYagAAAAEOVs+62Zadv2/uburzZizkoF7RBvcN+v1GORFlZ4ywl5U/ktVt0sSF2iKtVCsIxUWw==', '7733449', 'carljose@mgenitrack.com', '0788533226', 'Active', '2026-06-15 11:44:49', NULL, 1, NULL, NULL, b'0', NULL),
(29, 'Asano', 'Hassan', 'Male', 'AQAAAAIAAYagAAAAEBqnFIQ96cmzB91O4L37sDNEApIvii7B7yNmcyYal9XogPxzucveoyQnolLa6VTKZw==', '99663322', 'hassanoasan@mgenitrack.com', '0766553322', 'Active', '2026-06-15 11:47:36', NULL, 1, NULL, NULL, b'0', NULL),
(30, 'Salome', 'Njeri', 'Female', 'AQAAAAIAAYagAAAAEIxh+6IRq0Mh7MpdG+NtcK6KFEe0AtdbdSQO3M1TsX8L2KKLxRaZTLYpLfoDoGlaSg==', '5689392', 'salomenjeri@mgenitrack.com', '0799876443', 'Active', '2026-06-15 11:49:13', NULL, 1, NULL, NULL, b'0', NULL),
(31, 'Nanil', 'David', 'Male', 'AQAAAAIAAYagAAAAEIM52OM6zSa64cijC1AUxkeVQg/g74qiLIc6ThxbHHqkZNioTrwekedumHw6MFREnw==', '767229101', 'davidnaniii@mgenitrack.com', '0728892828', 'Active', '2026-06-15 11:50:22', NULL, 1, NULL, NULL, b'0', NULL),
(32, 'John', 'Kamau', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '34891725', 'john.kamau01@gmail.com', '0712845936', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(33, 'Mary', 'Wanjiku', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '56271894', 'mary.wanjiku02@gmail.com', '0791364825', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(34, 'Brian', 'Otieno', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '41736285', 'brian.otieno03@gmail.com', '0708945217', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(35, 'Faith', 'Achieng', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '92814573', 'faith.achieng04@gmail.com', '0723516849', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(36, 'Kevin', 'Mutua', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '35182947', 'kevin.mutua05@gmail.com', '0784152396', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(37, 'Diana', 'Njeri', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '84627135', 'diana.njeri06@gmail.com', '0719263548', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(38, 'Peter', 'Kiptoo', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '27519483', 'peter.kiptoo07@gmail.com', '0736829415', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(39, 'Mercy', 'Chebet', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '68194725', 'mercy.chebet08@gmail.com', '0792451863', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(40, 'Daniel', 'Mwangi', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '59372618', 'daniel.mwangi09@gmail.com', '0726483195', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(41, 'Grace', 'Wambui', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '71245896', 'grace.wambui10@gmail.com', '0715938264', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(42, 'James', 'Odhiambo', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '28461795', 'james.odhiambo11@gmail.com', '0746159283', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(43, 'Ann', 'Jepkoech', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '93628417', 'ann.jepkoech12@gmail.com', '0795182634', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(44, 'Samuel', 'Maina', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '51739482', 'samuel.maina13@gmail.com', '0721846395', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(45, 'Brenda', 'Atieno', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '74918536', 'brenda.atieno14@gmail.com', '0783629145', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(46, 'Victor', 'Karanja', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '36482719', 'victor.karanja15@gmail.com', '0718452936', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(47, 'Esther', 'Nyambura', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '82593174', 'esther.nyambura16@gmail.com', '0739164258', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(48, 'Dennis', 'Cheruiyot', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '41827395', 'dennis.cheruiyot17@gmail.com', '0793518426', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(49, 'Susan', 'Muthoni', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '69281435', 'susan.muthoni18@gmail.com', '0729361845', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(50, 'Collins', 'Koech', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '15374829', 'collins.koech19@gmail.com', '0781246953', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(51, 'Purity', 'Akinyi', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '83726149', 'purity.akinyi20@gmail.com', '0705283146', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(52, 'Eric', 'Kimani', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '52817394', 'eric.kimani21@gmail.com', '0714836295', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(53, 'Joy', 'Chepkemoi', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '71495283', 'joy.chepkemoi22@gmail.com', '0726148935', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(54, 'George', 'Ndegwa', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '28573641', 'george.ndegwa23@gmail.com', '0794251836', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(55, 'Lilian', 'Auma', 'Female', 'AQAAAAIAAYagAAAAEIMeaeoIBiulBMRENE0RmAuQGRtb+ViGnkbgj2zpRrt/bc2SDG3KOp8Ar94sIOof0Q==', '94628513', 'lilian.auma24@gmail.com', '0735164829', 'Active', '2026-06-15 12:26:08', '2026-06-15 13:00:57', 1, NULL, NULL, b'0', NULL),
(56, 'Joseph', 'Muriuki', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '63841572', 'joseph.muriuki25@gmail.com', '0712583946', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(57, 'Cynthia', 'Naliaka', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '47185296', 'cynthia.naliaka26@gmail.com', '0728631945', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(58, 'Martin', 'Barasa', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '82519473', 'martin.barasa27@gmail.com', '0791864253', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(59, 'Janet', 'Chepchirchir', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '35291864', 'janet.chepchirchir28@gmail.com', '0734286159', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(60, 'David', 'Kariuki', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '69418275', 'david.kariuki29@gmail.com', '0785632149', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(61, 'Alice', 'Nyongesa', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '14837295', 'alice.nyongesa30@gmail.com', '0719845263', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(62, 'Patrick', 'Omondi', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '85371624', 'patrick.omondi31@gmail.com', '0723519468', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(63, 'Lucy', 'Wairimu', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '46295183', 'lucy.wairimu32@gmail.com', '0739168245', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(64, 'Michael', 'Kibet', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '79152834', 'michael.kibet33@gmail.com', '0794512638', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(65, 'Irene', 'Jebet', 'Female', 'AQAAAAIAAYagAAAAEBw90+IACjE3UTl3Nn18P1QBpLbQzTY1T6acohNyRca95PJPATRCDoPEogdZ2926JQ==', '38527491', 'irene.jebet34@gmail.com', '0708264135', 'Active', '2026-06-15 12:26:08', '2026-06-15 13:03:29', 1, NULL, NULL, b'0', NULL),
(66, 'Stephen', 'Mwenda', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '62948157', 'stephen.mwenda35@gmail.com', '0725381946', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(67, 'Caroline', 'Nanjala', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '94731682', 'caroline.nanjala36@gmail.com', '0782946153', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(68, 'Kennedy', 'Rono', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '51392784', 'kennedy.rono37@gmail.com', '0716358294', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(69, 'Sharon', 'Chepkorir', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '87412635', 'sharon.chepkorir38@gmail.com', '0734829516', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(70, 'Paul', 'Macharia', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '26194837', 'paul.macharia39@gmail.com', '0792156483', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(71, 'Rose', 'Akinyi', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '63827195', 'rose.akinyi40@gmail.com', '0729483615', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(72, 'Fred', 'Mutiso', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '95271486', 'fred.mutiso41@gmail.com', '0713625849', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(73, 'Beatrice', 'Wanjiru', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '37491528', 'beatrice.wanjiru42@gmail.com', '0736152948', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(74, 'Simon', 'Kiplagat', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '68135297', 'simon.kiplagat43@gmail.com', '0784195263', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(75, 'Naomi', 'Chebet', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '42871635', 'naomi.chebet44@gmail.com', '0726153849', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(76, 'Chris', 'Were', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '89326174', 'chris.were45@gmail.com', '0798342165', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(77, 'Merceline', 'Auma', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '15739482', 'merceline.auma46@gmail.com', '0715268394', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(78, 'Allan', 'Mbugua', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '74936281', 'allan.mbugua47@gmail.com', '0739182645', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(79, 'Dorcas', 'Atieno', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '31528479', 'dorcas.atieno48@gmail.com', '0782635194', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(80, 'Elvis', 'Karanja', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '82647315', 'elvis.karanja49@gmail.com', '0726198453', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(81, 'Winfred', 'Nyokabi', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '59471826', 'winfred.nyokabi50@gmail.com', '0793456128', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(82, 'Nicholas', 'Mwangi', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '27486195', 'nicholas.mwangi51@gmail.com', '0718526349', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(83, 'Peris', 'Chepkemoi', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '81627354', 'peris.chepkemoi52@gmail.com', '0735249186', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(84, 'Joel', 'Owino', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '45189723', 'joel.owino53@gmail.com', '0781625934', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(85, 'Eunice', 'Wambui', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '92734618', 'eunice.wambui54@gmail.com', '0728491635', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(86, 'Andrew', 'Mumo', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '36821945', 'andrew.mumo55@gmail.com', '0793512864', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(87, 'Judith', 'Njeri', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '74518329', 'judith.njeri56@gmail.com', '0714639285', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(88, 'Boniface', 'Cheruiyot', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '58273914', 'boniface.cheruiyot57@gmail.com', '0739516248', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(89, 'Gladys', 'Akinyi', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '19384627', 'gladys.akinyi58@gmail.com', '0784159632', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(90, 'Felix', 'Maina', 'Male', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '86157294', 'felix.maina59@gmail.com', '0726183954', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL),
(91, 'Hellen', 'Chebet', 'Female', 'AQAAAAIAAYagAAAAEKL3rKMXndYUqVTg0ajUkfws454JzIPd6WbcP0yTVBU0BfqLig3+qARxLi6+QwLNRQ==', '43792816', 'hellen.chebet60@gmail.com', '0792864315', 'Active', '2026-06-15 12:26:08', NULL, 1, NULL, NULL, b'0', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `user_roles`
--

CREATE TABLE `user_roles` (
  `user_role_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `role_id` int(11) NOT NULL,
  `shift` enum('Day','Night') DEFAULT NULL,
  `user_status` enum('Active','Inactive') DEFAULT 'Active',
  `assigned_at` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user_roles`
--

INSERT INTO `user_roles` (`user_role_id`, `user_id`, `role_id`, `shift`, `user_status`, `assigned_at`) VALUES
(1, 1, 1, NULL, 'Active', '2026-02-13 13:36:12'),
(4, 4, 3, 'Day', 'Active', '2026-03-03 13:57:45'),
(6, 10, 2, NULL, 'Active', '2026-03-22 14:36:44'),
(7, 11, 4, NULL, 'Active', '2026-03-22 14:41:02'),
(8, 12, 4, NULL, 'Active', '2026-03-25 15:04:11'),
(9, 13, 4, NULL, 'Active', '2026-03-25 15:05:32'),
(10, 14, 3, 'Day', 'Active', '2026-03-26 13:26:35'),
(11, 15, 4, NULL, 'Active', '2026-04-23 12:48:33'),
(12, 16, 4, NULL, 'Active', '2026-04-24 10:53:03'),
(13, 17, 4, NULL, 'Active', '2026-04-29 13:07:39'),
(14, 18, 4, NULL, 'Active', '2026-05-01 13:58:06'),
(15, 19, 4, NULL, 'Active', '2026-05-02 01:24:33'),
(16, 20, 3, 'Night', 'Active', '2026-05-03 14:35:18'),
(17, 21, 4, NULL, 'Active', '2026-05-04 16:42:23'),
(18, 22, 4, NULL, 'Active', '2026-05-04 17:51:55'),
(19, 23, 4, NULL, 'Active', '2026-05-04 18:50:13'),
(20, 24, 4, NULL, 'Active', '2026-05-06 13:23:56'),
(21, 25, 4, NULL, 'Active', '2026-05-06 14:46:43'),
(22, 26, 4, NULL, 'Active', '2026-05-20 14:09:48'),
(23, 27, 3, 'Night', 'Active', '2026-06-15 11:43:13'),
(24, 28, 4, NULL, 'Active', '2026-06-15 11:44:49'),
(25, 29, 4, NULL, 'Active', '2026-06-15 11:47:36'),
(26, 30, 4, NULL, 'Active', '2026-06-15 11:49:13'),
(27, 31, 4, NULL, 'Active', '2026-06-15 11:50:22'),
(28, 55, 4, NULL, 'Active', '2026-06-15 13:00:41'),
(29, 65, 4, NULL, 'Active', '2026-06-15 13:03:03');

--
-- Triggers `user_roles`
--
DELIMITER $$
CREATE TRIGGER `limit_single_roles` BEFORE INSERT ON `user_roles` FOR EACH ROW BEGIN
    DECLARE roleName VARCHAR(50);
    DECLARE roleCount INT;

    SELECT role_name INTO roleName
    FROM ROLES
    WHERE role_id = NEW.role_id;

    IF roleName = 'SuperAdmin' THEN
        SELECT COUNT(*) INTO roleCount
        FROM USER_ROLES ur
        JOIN ROLES r ON ur.role_id = r.role_id
        WHERE r.role_name = 'SuperAdmin';

        IF roleCount >= 1 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Only one SuperAdmin allowed.';
        END IF;
    END IF;

    IF roleName = 'PropertyManager' THEN
        SELECT COUNT(*) INTO roleCount
        FROM USER_ROLES ur
        JOIN ROLES r ON ur.role_id = r.role_id
        WHERE r.role_name = 'PropertyManager';

        IF roleCount >= 1 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Only one Property Manager allowed.';
        END IF;
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `visitors`
--

CREATE TABLE `visitors` (
  `visitor_id` int(11) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `id_number` varchar(50) DEFAULT NULL,
  `contact_number` varchar(20) DEFAULT NULL,
  `photo_url` varchar(500) DEFAULT NULL,
  `first_visit_date` datetime DEFAULT NULL,
  `total_visits` int(11) DEFAULT 0,
  `created_at` datetime DEFAULT current_timestamp(),
  `updated_at` datetime DEFAULT NULL,
  `invited_via_invitation_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `visitors`
--

INSERT INTO `visitors` (`visitor_id`, `full_name`, `id_number`, `contact_number`, `photo_url`, `first_visit_date`, `total_visits`, `created_at`, `updated_at`, `invited_via_invitation_id`) VALUES
(1, 'Daktari Mercy Asochi', '567890', '0789652889', NULL, '2026-02-21 13:23:00', 0, '2026-02-21 13:23:47', '2026-02-21 13:23:00', NULL),
(2, 'Casy Kiuna', '45678598', '0745289794', NULL, '2026-03-22 14:48:16', 1, '2026-03-22 14:48:16', NULL, 1),
(4, 'Chris Kirubi', '6765436', '080024531', NULL, '2026-03-25 15:10:35', 1, '2026-03-25 15:10:35', NULL, 2),
(6, 'Muthama Michelle', '5692015', '0732897654', NULL, '2026-03-25 16:09:58', 1, '2026-03-25 16:09:58', NULL, NULL),
(7, 'Dianne Abby', '68902839', '0754389124', NULL, '2026-03-25 16:13:32', 1, '2026-03-25 16:13:32', NULL, 3),
(8, 'Helena Mercy', '6789204', '0768295411', NULL, '2026-03-26 13:28:08', 1, '2026-03-26 13:28:08', NULL, NULL),
(9, 'Shiko Rakel', '6789015', '0745286709', NULL, '2026-04-22 15:25:34', 1, '2026-04-22 15:25:34', NULL, NULL),
(10, 'Titus Anyona', '349088', '0789099976', '/uploads/visitors/0e6333a3-278e-49be-bf67-08e35f3e88b0.jfif', '2026-04-23 12:06:38', 2, '2026-04-23 12:06:38', '2026-04-23 12:06:55', NULL),
(11, 'Nameless Kagwe', '78902787', '089220918', NULL, '2026-04-23 12:55:49', 1, '2026-04-23 12:55:49', NULL, 4),
(12, 'Isaac Kagwe', '1234', '09867890', NULL, '2026-04-24 10:54:59', 1, '2026-04-24 10:54:59', NULL, 5),
(13, 'Paul Kimani', '9082927', '0789236784', '/uploads/visitors/d799999e-cf6a-491e-bb36-95efb7c1a875.jpg', '2026-04-29 13:19:35', 1, '2026-04-29 13:19:35', NULL, NULL),
(14, 'Alice Wariara', '7890267', '0734567829', '/uploads/visitors/a6870353-d4fd-457f-98ab-9da5c6b80bbd.jpg', '2026-05-01 10:54:18', 1, '2026-05-01 10:54:18', NULL, NULL),
(15, 'Wendy Wariara', '9873566', '0790236481', '/uploads/visitors/e31fb5a4-9426-4208-8ab9-1e0b1256b2ff.jpg', '2026-05-01 11:30:24', 1, '2026-05-01 11:30:24', NULL, NULL),
(16, 'Alice Kioko', '7802938', '0713693079', NULL, '2026-05-01 11:34:58', 1, '2026-05-01 11:34:58', NULL, 6),
(17, 'Ryan Wakeba', '54678921', '0756437899', '/uploads/visitors/5f5209a2-abfc-481f-8a50-b17d284e074d.jpg', '2026-05-02 01:19:34', 1, '2026-05-02 01:19:34', NULL, NULL),
(18, 'Rexy  Muthama', '3245189', '071245673', '/uploads/visitors/27a4c053-ce04-4471-a331-f7264673ebcf.jpg', '2026-05-03 14:37:36', 1, '2026-05-03 14:37:36', NULL, NULL),
(19, 'Shiko Stylist', '34788911', '0728994377', '/uploads/visitors/fa46fac5-d5a7-4860-b274-845c9966f23a.jpg', '2026-05-04 16:26:35', 1, '2026-05-04 16:26:35', NULL, NULL),
(20, 'Susan Odum', '78920211', '0715567163', '/uploads/visitors/1212f384-74ba-4d0d-9bb2-aa7dd2755647.jpg', '2026-05-06 15:23:13', 1, '2026-05-06 15:23:13', NULL, NULL),
(21, 'Rebecca Rose', '3820843', '0745289794', NULL, '2026-05-06 15:27:03', 1, '2026-05-06 15:27:03', NULL, 7),
(22, 'Paul Walkerr', '48399028', '0728229018', NULL, '2026-05-09 14:39:39', 1, '2026-05-09 14:39:39', NULL, 8),
(23, 'Rex Maasai', '5566789', '0111253799', NULL, '2026-06-15 13:05:16', 1, '2026-06-15 13:05:17', NULL, 10),
(24, 'Michael Kinyua', '17893999', '078922356', NULL, '2026-06-17 10:49:00', 1, '2026-06-17 10:49:00', NULL, 12),
(25, 'Barasa Joel', '4536789', '0738822999', '/uploads/visitors/846018fc-7ad6-4fc9-9dd2-5d3425dc9555.jpg', '2026-06-17 10:52:00', 1, '2026-06-17 10:52:00', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `visitor_invitations`
--

CREATE TABLE `visitor_invitations` (
  `invitation_id` int(11) NOT NULL,
  `resident_id` int(11) NOT NULL,
  `visitor_name` varchar(100) NOT NULL,
  `visitor_phone` varchar(20) DEFAULT NULL,
  `visitor_email` varchar(100) DEFAULT NULL,
  `purpose_of_visit` varchar(255) DEFAULT NULL,
  `expected_date` date DEFAULT NULL,
  `invitation_token` varchar(255) DEFAULT NULL,
  `qr_code_path` varchar(255) DEFAULT NULL,
  `visit_status` enum('Pending','Arrived','Expired','Cancelled') DEFAULT 'Pending',
  `created_at` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `visitor_invitations`
--

INSERT INTO `visitor_invitations` (`invitation_id`, `resident_id`, `visitor_name`, `visitor_phone`, `visitor_email`, `purpose_of_visit`, `expected_date`, `invitation_token`, `qr_code_path`, `visit_status`, `created_at`) VALUES
(1, 2, 'Casy Kiuna', '0745289794', 'joyritanjoki061@gmail.com', 'Personal Visit', '2026-03-22', 'CD1D6F6370', '/qrcodes/CD1D6F6370.png', 'Arrived', '2026-03-22 14:42:21'),
(2, 4, 'Chris Kirubi', '080024531', 'kirubi23@gmail.com', 'Family', '2026-03-25', '66D0A3553F', '/qrcodes/66D0A3553F.png', 'Arrived', '2026-03-25 15:06:31'),
(3, 4, 'Dianne Abby', '0754389124', 'diabby@gmail.com', 'Business', '2026-03-25', '88E7A57561', '/qrcodes/88E7A57561.png', 'Arrived', '2026-03-25 16:12:21'),
(4, 5, 'Nameless Kagwe', '089220918', 'nameless@gmail.com', 'Personal Visit', '2026-04-23', '86EA77A135', '/qrcodes/86EA77A135.png', 'Arrived', '2026-04-23 12:54:51'),
(5, 6, 'Isaac Kagwe', '09867890', 'kagwe@gmail.com', 'Personal Visit', '2026-04-24', '92DFA7FE8A', '/qrcodes/92DFA7FE8A.png', 'Arrived', '2026-04-24 10:53:54'),
(6, 2, 'Alice Kioko', '0713693079', 'alicek@gmail.com', 'Personal Visit', '2026-05-01', '6974032A65', '/qrcodes/6974032A65.png', 'Arrived', '2026-05-01 11:33:07'),
(7, 11, 'Rebecca Rose', '0745289794', 'rebecser@gmail.com', 'BnB Stay', '2026-05-06', '56AF98A230', '/qrcodes/56AF98A230.png', 'Arrived', '2026-05-06 15:25:08'),
(8, 2, 'Paul Walkerr', '0728229018', 'walkerpau@mgenitrack.com', 'Personal Visit', '2026-05-09', '672AF94149', '/qrcodes/672AF94149.png', 'Arrived', '2026-05-09 14:38:31'),
(9, 16, 'Sharon Nyambura', '0789928287', 'nyambura@gmail.com', 'Personal Visit', '2026-06-11', '6F2312933B', '/qrcodes/6F2312933B.png', 'Expired', '2026-06-11 09:50:00'),
(10, 28, 'Rex Maasai', '0111253799', 'rexauma@gmail.com', 'Family', '2026-06-15', 'D70CBB00C2', '/qrcodes/D70CBB00C2.png', 'Arrived', '2026-06-15 13:01:43'),
(11, 50, 'Noni Gathoni', '0789921112', 'nonogathosh@gmail.com', 'BnB Stay', '2026-06-15', '6A24FA87F4', '/qrcodes/6A24FA87F4.png', 'Expired', '2026-06-15 13:04:08'),
(12, 10, 'Michael Kinyua', '078922356', 'michael@mgenitrack.com', 'BnB Stay', '2026-06-17', '5B215D9356', '/qrcodes/5B215D9356.png', 'Arrived', '2026-06-17 10:48:07');

-- --------------------------------------------------------

--
-- Table structure for table `visits`
--

CREATE TABLE `visits` (
  `visit_id` int(11) NOT NULL,
  `visitor_id` int(11) NOT NULL,
  `checked_in_by` int(11) DEFAULT NULL,
  `checked_out_by` int(11) DEFAULT NULL,
  `house_number` varchar(20) DEFAULT NULL,
  `purpose_of_visit` varchar(255) DEFAULT NULL,
  `car_registration` varchar(50) DEFAULT NULL,
  `number_of_occupants` int(11) DEFAULT 1,
  `time_in` datetime NOT NULL,
  `time_out` datetime DEFAULT NULL,
  `visit_status` enum('CheckedIn','CheckedOut') DEFAULT 'CheckedIn',
  `qr_code` varchar(255) DEFAULT NULL,
  `qr_token` varchar(100) DEFAULT NULL,
  `visit_duration` int(11) DEFAULT NULL,
  `created_at` datetime DEFAULT current_timestamp(),
  `invitation_id` int(11) DEFAULT NULL,
  `check_in_method` enum('QR','Manual') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `visits`
--

INSERT INTO `visits` (`visit_id`, `visitor_id`, `checked_in_by`, `checked_out_by`, `house_number`, `purpose_of_visit`, `car_registration`, `number_of_occupants`, `time_in`, `time_out`, `visit_status`, `qr_code`, `qr_token`, `visit_duration`, `created_at`, `invitation_id`, `check_in_method`) VALUES
(1, 2, 4, 4, 'A606', 'Personal Visit', NULL, 1, '2026-03-22 14:48:17', '2026-03-25 15:14:59', 'CheckedOut', NULL, NULL, 4346, '2026-03-22 14:48:17', 1, 'QR'),
(2, 4, 4, 14, 'A101', 'Family', NULL, 1, '2026-03-25 15:10:35', '2026-03-26 13:28:33', 'CheckedOut', NULL, NULL, 1337, '2026-03-25 15:10:35', 2, 'QR'),
(3, 6, 4, 14, 'B505', 'Family', NULL, 1, '2026-03-25 16:09:58', '2026-04-24 10:55:19', 'CheckedOut', NULL, NULL, 42885, '2026-03-25 16:09:58', NULL, 'Manual'),
(4, 7, 4, 4, 'A101', 'Business', NULL, 1, '2026-03-25 16:13:32', '2026-05-01 11:29:23', 'CheckedOut', NULL, NULL, 52995, '2026-03-25 16:13:32', 3, 'QR'),
(5, 8, 14, 4, 'C502', 'BnB Stay', 'KDE 234F', 1, '2026-03-26 13:28:08', '2026-04-23 12:03:35', 'CheckedOut', NULL, NULL, 40235, '2026-03-26 13:28:08', NULL, 'Manual'),
(6, 9, 4, 4, 'C305', 'BnB Stay', 'KDX 345M', 2, '2026-04-22 15:25:34', '2026-04-23 12:03:42', 'CheckedOut', NULL, NULL, 1238, '2026-04-22 15:25:34', NULL, 'Manual'),
(7, 11, 14, 4, 'B203', 'Personal Visit', NULL, 1, '2026-04-23 12:55:49', '2026-05-01 11:29:20', 'CheckedOut', NULL, NULL, 11433, '2026-04-23 12:55:49', 4, 'QR'),
(8, 12, 14, 4, 'C508', 'Personal Visit', NULL, 1, '2026-04-24 10:54:59', '2026-04-29 13:16:50', 'CheckedOut', NULL, NULL, 7341, '2026-04-24 10:54:59', 5, 'QR'),
(9, 15, 4, 20, 'A606', 'Personal Visit', NULL, 1, '2026-05-01 11:30:24', '2026-05-03 14:36:19', 'CheckedOut', NULL, NULL, 3065, '2026-05-01 11:30:24', NULL, 'Manual'),
(10, 16, 14, 20, 'A606', 'Personal Visit', NULL, 1, '2026-05-01 11:34:58', '2026-05-03 14:36:13', 'CheckedOut', NULL, NULL, 3061, '2026-05-01 11:34:58', 6, 'QR'),
(11, 17, 4, 20, 'A606', 'Personal Visit', NULL, 1, '2026-05-02 01:19:34', '2026-05-03 14:36:16', 'CheckedOut', NULL, NULL, 2236, '2026-05-02 01:19:34', NULL, 'Manual'),
(12, 18, 20, 20, 'B505', 'Maintenance / Repair', NULL, 1, '2026-05-03 14:37:36', '2026-05-04 18:01:15', 'CheckedOut', NULL, NULL, 1643, '2026-05-03 14:37:36', NULL, 'Manual'),
(13, 19, 14, 14, 'B505', 'Family', NULL, 1, '2026-05-04 16:26:35', '2026-05-06 15:21:08', 'CheckedOut', NULL, NULL, 2814, '2026-05-04 16:26:35', NULL, 'Manual'),
(14, 20, 20, 14, 'C104', 'BnB Stay', 'KBQ 334R', 1, '2026-05-06 15:23:13', '2026-05-09 13:50:17', 'CheckedOut', NULL, NULL, 4227, '2026-05-06 15:23:13', NULL, 'Manual'),
(15, 21, 20, 14, 'C104', 'BnB Stay', NULL, 1, '2026-05-06 15:27:03', '2026-05-09 13:50:14', 'CheckedOut', NULL, NULL, 4223, '2026-05-06 15:27:03', 7, 'QR'),
(16, 22, 14, 14, 'A606', 'Personal Visit', NULL, 1, '2026-05-09 14:39:39', '2026-05-20 13:39:05', 'CheckedOut', NULL, NULL, 15779, '2026-05-09 14:39:39', 8, 'QR'),
(17, 23, 14, NULL, 'B309', 'Family', NULL, 1, '2026-06-15 13:05:17', NULL, 'CheckedIn', NULL, NULL, NULL, '2026-06-15 13:05:17', 10, 'QR'),
(18, 24, 14, NULL, 'C201', 'BnB Stay', NULL, 1, '2026-06-17 10:49:00', NULL, 'CheckedIn', NULL, NULL, NULL, '2026-06-17 10:49:00', 12, 'QR'),
(19, 25, 14, NULL, 'B503', 'Delivery', NULL, 1, '2026-06-17 10:52:00', NULL, 'CheckedIn', NULL, NULL, NULL, '2026-06-17 10:52:00', NULL, 'Manual');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `activity_logs`
--
ALTER TABLE `activity_logs`
  ADD PRIMARY KEY (`log_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `notifications`
--
ALTER TABLE `notifications`
  ADD PRIMARY KEY (`notification_id`),
  ADD KEY `resident_id` (`resident_id`),
  ADD KEY `visit_id` (`visit_id`);

--
-- Indexes for table `reports`
--
ALTER TABLE `reports`
  ADD PRIMARY KEY (`report_id`),
  ADD KEY `generated_by` (`generated_by`);

--
-- Indexes for table `residents`
--
ALTER TABLE `residents`
  ADD PRIMARY KEY (`resident_id`),
  ADD UNIQUE KEY `user_id` (`user_id`),
  ADD KEY `fk_residents_unit` (`unit_id`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`role_id`),
  ADD UNIQUE KEY `role_name` (`role_name`);

--
-- Indexes for table `system_settings`
--
ALTER TABLE `system_settings`
  ADD PRIMARY KEY (`setting_id`),
  ADD UNIQUE KEY `setting_key` (`setting_key`),
  ADD KEY `updated_by` (`updated_by`);

--
-- Indexes for table `units`
--
ALTER TABLE `units`
  ADD PRIMARY KEY (`unit_id`),
  ADD UNIQUE KEY `unit_number` (`unit_number`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `id_number` (`id_number`),
  ADD KEY `created_by` (`created_by`);

--
-- Indexes for table `user_roles`
--
ALTER TABLE `user_roles`
  ADD PRIMARY KEY (`user_role_id`),
  ADD UNIQUE KEY `user_id` (`user_id`,`role_id`),
  ADD KEY `role_id` (`role_id`);

--
-- Indexes for table `visitors`
--
ALTER TABLE `visitors`
  ADD PRIMARY KEY (`visitor_id`),
  ADD KEY `invited_via_invitation_id` (`invited_via_invitation_id`);

--
-- Indexes for table `visitor_invitations`
--
ALTER TABLE `visitor_invitations`
  ADD PRIMARY KEY (`invitation_id`),
  ADD UNIQUE KEY `invitation_token` (`invitation_token`),
  ADD KEY `resident_id` (`resident_id`);

--
-- Indexes for table `visits`
--
ALTER TABLE `visits`
  ADD PRIMARY KEY (`visit_id`),
  ADD KEY `visitor_id` (`visitor_id`),
  ADD KEY `checked_in_by` (`checked_in_by`),
  ADD KEY `checked_out_by` (`checked_out_by`),
  ADD KEY `invitation_id` (`invitation_id`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `activity_logs`
--
ALTER TABLE `activity_logs`
  MODIFY `log_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=242;

--
-- AUTO_INCREMENT for table `notifications`
--
ALTER TABLE `notifications`
  MODIFY `notification_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `reports`
--
ALTER TABLE `reports`
  MODIFY `report_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `residents`
--
ALTER TABLE `residents`
  MODIFY `resident_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=51;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `system_settings`
--
ALTER TABLE `system_settings`
  MODIFY `setting_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `units`
--
ALTER TABLE `units`
  MODIFY `unit_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=181;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=92;

--
-- AUTO_INCREMENT for table `user_roles`
--
ALTER TABLE `user_roles`
  MODIFY `user_role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `visitors`
--
ALTER TABLE `visitors`
  MODIFY `visitor_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `visitor_invitations`
--
ALTER TABLE `visitor_invitations`
  MODIFY `invitation_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `visits`
--
ALTER TABLE `visits`
  MODIFY `visit_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `activity_logs`
--
ALTER TABLE `activity_logs`
  ADD CONSTRAINT `activity_logs_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `notifications`
--
ALTER TABLE `notifications`
  ADD CONSTRAINT `notifications_ibfk_1` FOREIGN KEY (`resident_id`) REFERENCES `residents` (`resident_id`),
  ADD CONSTRAINT `notifications_ibfk_2` FOREIGN KEY (`visit_id`) REFERENCES `visits` (`visit_id`);

--
-- Constraints for table `reports`
--
ALTER TABLE `reports`
  ADD CONSTRAINT `reports_ibfk_1` FOREIGN KEY (`generated_by`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `residents`
--
ALTER TABLE `residents`
  ADD CONSTRAINT `fk_residents_unit` FOREIGN KEY (`unit_id`) REFERENCES `units` (`unit_id`) ON DELETE SET NULL,
  ADD CONSTRAINT `residents_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `system_settings`
--
ALTER TABLE `system_settings`
  ADD CONSTRAINT `system_settings_ibfk_1` FOREIGN KEY (`updated_by`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`created_by`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `user_roles`
--
ALTER TABLE `user_roles`
  ADD CONSTRAINT `user_roles_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `user_roles_ibfk_2` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`) ON DELETE CASCADE;

--
-- Constraints for table `visitors`
--
ALTER TABLE `visitors`
  ADD CONSTRAINT `visitors_ibfk_1` FOREIGN KEY (`invited_via_invitation_id`) REFERENCES `visitor_invitations` (`invitation_id`);

--
-- Constraints for table `visitor_invitations`
--
ALTER TABLE `visitor_invitations`
  ADD CONSTRAINT `visitor_invitations_ibfk_1` FOREIGN KEY (`resident_id`) REFERENCES `residents` (`resident_id`);

--
-- Constraints for table `visits`
--
ALTER TABLE `visits`
  ADD CONSTRAINT `visits_ibfk_1` FOREIGN KEY (`visitor_id`) REFERENCES `visitors` (`visitor_id`),
  ADD CONSTRAINT `visits_ibfk_2` FOREIGN KEY (`checked_in_by`) REFERENCES `users` (`user_id`),
  ADD CONSTRAINT `visits_ibfk_3` FOREIGN KEY (`checked_out_by`) REFERENCES `users` (`user_id`),
  ADD CONSTRAINT `visits_ibfk_4` FOREIGN KEY (`invitation_id`) REFERENCES `visitor_invitations` (`invitation_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
