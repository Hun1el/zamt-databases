-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Nov 22, 2024 at 04:12 PM
-- Server version: 8.0.30
-- PHP Version: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `PR11_Solonikov`
--

-- --------------------------------------------------------

--
-- Table structure for table `salesreceipt`
--

CREATE TABLE `salesreceipt` (
  `id` int NOT NULL,
  `name` varchar(100) NOT NULL,
  `amount` int NOT NULL,
  `price` int NOT NULL,
  `date` date NOT NULL,
  `totalprice` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `salesreceipt`
--

INSERT INTO `salesreceipt` (`id`, `name`, `amount`, `price`, `date`, `totalprice`) VALUES
(1, 'Витамины', 2, 5, '2024-11-01', 10),
(2, 'Витамины', 1, 7, '2024-11-02', 7),
(3, 'Витамины', 3, 6, '2024-11-03', 18),
(4, 'Витамины', 1, 15, '2024-11-04', 15),
(5, 'Витамины', 4, 3, '2024-11-05', 12),
(6, 'Витамины', 2, 8, '2024-11-06', 16),
(7, 'Витамины', 1, 12, '2024-11-07', 12),
(8, 'Витамины', 2, 25, '2024-11-08', 50),
(9, 'Витамины', 1, 9, '2024-11-09', 9),
(10, 'Витамины', 3, 4, '2024-11-10', 12);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `salesreceipt`
--
ALTER TABLE `salesreceipt`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `salesreceipt`
--
ALTER TABLE `salesreceipt`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
