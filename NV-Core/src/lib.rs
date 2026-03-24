#[no_mangle]
pub extern "C" fn calculate_combat_power(bots: i32, power_level: i32, treasury: f64) -> f64 {
    let base = (bots as f64 * power_level as f64) * 1.5;
    let bonus = treasury * 0.05;
    base + bonus
}

#[no_mangle]
pub extern "C" fn check_core_status() {
    println!("[NV-CORE] Rust Engine is Online. Ryzen 12-Threads Ready.");
}